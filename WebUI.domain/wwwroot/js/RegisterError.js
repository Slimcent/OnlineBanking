
    const firstName = document.getElementsByClassName('firstName')[0];
    const lastName = document.querySelector('lastName');
    const email = document.querySelector('.email');
    const birthday = document.querySelector('.birthday');
    const gender = document.querySelector('.gender');
    const accountType = document.querySelector('.accountType');

    let firstNameError = document.querySelector('.firstNameError');
    let lastNameError = document.querySelector('.lastNameError');
    let emailError = document.querySelector('.emailError');
    let birthdayErrorr = document.querySelector('.birthdayError');
    let genderError = document.querySelector('.genderError');
    let accountTypeError = document.querySelector('.accountTypeError');

    if (firstName) {
        firstName.addEventListener('focus', function () {
            firstNameError.innerHTML = "";
            lastNameError.innerHTML = "";
            emailError.innerHTML = "";
            birthdayErrorr.innerHTML = "";
            genderError.innerHTML = "";
            accountTypeError.innerHTML = "";
        });
    }

if (lastName) {
    lastName.addEventListener('focus', (e) => {
        firstNameError.innerHTML = "";
        lastNameError.innerHTML = "";
        emailError.innerHTML = "";
        birthdayErrorr.innerHTML = "";
        genderError.innerHTML = "";
        accountTypeError.innerHTML = "";
    });
}

if (email) {
    email.addEventListener('focus', (e) => {
        firstNameError.innerHTML = "";
        lastNameError.innerHTML = "";
        emailError.innerHTML = "";
        birthdayErrorr.innerHTML = "";
        genderError.innerHTML = "";
        accountTypeError.innerHTML = "";
    });
}
    

if (birthday) {
    birthday.addEventListener('focus', (e) => {
        firstNameError.innerHTML = "";
        lastNameError.innerHTML = "";
        emailError.innerHTML = "";
        birthdayErrorr.innerHTML = "";
        genderError.innerHTML = "";
        accountTypeError.innerHTML = "";
    });
}
    

if (gender) {
    gender.addEventListener('focus', (e) => {
        firstNameError.innerHTML = "";
        lastNameError.innerHTML = "";
        emailError.innerHTML = "";
        birthdayErrorr.innerHTML = "";
        genderError.innerHTML = "";
        accountTypeError.innerHTML = "";
    });
}


if (accountType) {
    accountType.addEventListener('focus', (e) => {
        firstNameError.innerHTML = "";
        lastNameError.innerHTML = "";
        emailError.innerHTML = "";
        birthdayErrorr.innerHTML = "";
        genderError.innerHTML = "";
        accountTypeError.innerHTML = "";
    });
}

    



