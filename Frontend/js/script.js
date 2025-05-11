async function fetchPublicKey() {
  const res = await fetch("https://localhost:5024/api/crypto/public-key");
  const jwk = await res.json();

  return await crypto.subtle.importKey(
    "jwk",
    {
      ...jwk,
      n: jwk.n,
      e: jwk.e,
    },
    { name: "RSA-OAEP", hash: "SHA-256" },
    true,
    ["encrypt"]
  );
}
async function encryptPayload(data) {
  const publicKey = await fetchPublicKey();

  const encoded = new TextEncoder().encode(JSON.stringify(data));
  const encrypted = await crypto.subtle.encrypt(
    { name: "RSA-OAEP" },
    publicKey,
    encoded
  );

  return btoa(String.fromCharCode(...new Uint8Array(encrypted))); // Base64 encode for sending
}
document
  .getElementById("registerForm")
  .addEventListener("submit", async function (e) {
    e.preventDefault();

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const encrypted = await encryptPayload({ email, password });

    fetch("https://localhost:5024/api/auth/register", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ encrypted }),
    });
  });
