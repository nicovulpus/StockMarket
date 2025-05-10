document
  .getElementById("registerForm")
  .addEventListener("submit", function (e) {
    const password = document.getElementById("password").value;
    const confirm = document.getElementById("confirmPassword").value;
    const errorDisplay = document.getElementById("error");

    // Password validation
    const isLongEnough = password.length >= 8;
    const hasDigit = /\d/.test(password);
    const hasUppercase = /[A-Z]/.test(password);
    const hasLowercase = /[a-z]/.test(password);

    // Clear previous error
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
