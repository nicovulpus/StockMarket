// CHECKING THE PASSWORD REQUIREMENTS

document
  .getElementById("registerForm")
  .addEventListener("submit", function (e) {
    const password = document.getElementById("password").value;
    const confirm = document.getElementById("confirmPassword").value;
    const errorDisplay = document.getElementById("error");

    const isLongEnough = password.length >= 8;
    const hasDigit = /\d/.test(password);
    const hasUppercase = /[A-Z]/.test(password);
    const hasLowercase = /[a-z]/.test(password);

    errorDisplay.textContent = "";

    if (!isLongEnough || !hasDigit || !hasUppercase) {
      e.preventDefault();
      errorDisplay.textContent =
        "Password must be at least 8 characters long, contain at least one number, one uppercase letter and one lowercase letter.";
      return;
    }

    if (password !== confirm) {
      e.preventDefault();
      errorDisplay.textContent = "Passwords do not match.";
      return;
    }
  });

//SEND FORM REGISTER FORM TO BACKEND
document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("registerForm");

  form.addEventListener("submit", async (e) => {
    e.preventDefault();
    console.log("Register form submitted!");

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    try {
      const publicKey = await fetchPublicKey(); // 🟢 THIS is where you set it
      if (!publicKey) throw new Error("Public key is undefined");

      const encrypted = await encryptPayload({ email, password }, publicKey);

      const response = await fetch("https://localhost:7236/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ encrypted }),
      });

      const result = await response.json();
      console.log("✅ Server response:", result);
    } catch (err) {
      console.error("❌ Registration failed:", err);
    }
  });
});

async function fetchPublicKey() {
  try {
    const res = await fetch("https://localhost:7236/api/crypto/public-key");
    const jwkRaw = await res.json();
    console.log("📦 JWK from server:", jwkRaw);

    // Strip extra fields that cause silent importKey failure
    const { kty, n, e, ext } = jwkRaw;
    const cleanJwk = { kty, n, e, ext };

    const publicKey = await window.crypto.subtle.importKey(
      "jwk",
      cleanJwk,
      {
        name: "RSA-OAEP",
        hash: "SHA-256",
      },
      true,
      ["encrypt"]
    );

    console.log("✅ Imported key:", publicKey);
    return publicKey;
  } catch (error) {
    console.error("❌ Key import failed:", error);
    return undefined;
  }
}

async function encryptPayload(data, publicKey) {
  if (!publicKey) {
    throw new Error("Public key is undefined");
  }

  const encoded = new TextEncoder().encode(JSON.stringify(data));

  const encryptedBuffer = await crypto.subtle.encrypt(
    { name: "RSA-OAEP" },
    publicKey,
    encoded
  );

  return btoa(String.fromCharCode(...new Uint8Array(encryptedBuffer)));
}
