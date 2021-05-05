let isUserLoggedIn = false;

let viewModel = {};

const loginViewModel = {
    email: '',
    password: ''
};

const registerViewModel = {
    email: '',
    password: '',
    username: '',
    displayName: ''
};

const showSpinner = () => {
    $("#loadingModal").modal("show");
};

const hideSpinner = () => {
    $("#loadingModal").modal("hide");
};

const showToast = (message) => {
    toastr.info(message);
    hideSpinner();
};

const showErrorToast = (errorMessage) => {
    toastr.error(errorMessage);
    hideSpinner();
};

const setTokenInClient = (token) => {
    window.localStorage.clear();
    window.localStorage.setItem("jwtToken", token);
};

const getCompanyData = () => {
    $.ajax({
        url: "/api/companies",
        type: "GET",
        headers: { "Authorization": 'Bearer ' + window.localStorage.getItem('jwtToken') },
        success: function (response) {
            viewModel.companyData([]);
            viewModel.companyData(response);
            hideSpinner();
            $("#divData").show("slow");
        },
        error: function (errorResponse) {
            console.log(errorResponse);
            showErrorToast(errorResponse.responseText);
        }
    });
};

const getCompanyDataById = (id) => {
    $.ajax({
        url: "/api/companies/" + id,
        type: "GET",
        headers: { "Authorization": 'Bearer ' + window.localStorage.getItem('jwtToken') },
        success: function (response) {
            viewModel.companyData([]);
            viewModel.companyData(response);
            hideSpinner();
            $("#divData").show("slow");
        },
        error: function (errorResponse) {
            console.log(errorResponse);
            showErrorToast(errorResponse.responseText);
        }
    });
};

const getCompanyDataByIsin = (isin) => {
    $.ajax({
        url: "/api/companies/" + isin + "/GetCompanyByIsin",
        type: "GET",
        headers: { "Authorization": 'Bearer ' + window.localStorage.getItem('jwtToken') },
        success: function (response) {
            viewModel.companyData([]);
            viewModel.companyData(response);
            hideSpinner();
            $("#divData").show("slow");
        },
        error: function (errorResponse) {
            console.log(errorResponse);
            showErrorToast(errorResponse.responseText);
        }
    });
};

const editCompanyData = (data) => {
    $.ajax({
        url: "/api/Companies/",
        type: "PUT",
        headers: { "Authorization": 'Bearer ' + window.localStorage.getItem('jwtToken') },
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response) {
            hideSpinner();
            showToast(response);
            getCompanyData();
        },
        error: function (errorResponse) {
            console.log(errorResponse);
            showErrorToast(errorResponse.responseText);
        }
    });
};

const createCompanyData = (data) => {
    $.ajax({
        url: "/api/Companies/",
        type: "POST",
        headers: { "Authorization": 'Bearer ' + window.localStorage.getItem('jwtToken') },
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response) {
            hideSpinner();
            showToast(response);
            getCompanyData();
        },
        error: function (errorResponse) {
            console.log(errorResponse);
            showErrorToast(errorResponse.responseText);
        }
    });
};

const reloadPage = () => {
    setTimeout(function () {
        window.location.reload();
    }, 1000);
};

const submitLoginForm = (loginData) => {
    $.ajax({
        url: "/api/account/login",
        type: "POST",
        data: JSON.stringify(loginData),
        contentType: "application/json",
        success: function (response) {
            hideSpinner();
            const token = response.token;
            setTokenInClient(token);
            $("#divData").show();
        },
        error: function (errorResponse) {
            console.log(errorResponse);
            showErrorToast(errorResponse.responseText);
            setTimeout(function () {
                window.location.reload();
            }, 2000);
        }
    });
};

const submitRegisterForm = (registerData) => {
    $.ajax({
        url: "/api/account/register",
        type: "POST",
        data: JSON.stringify(registerData),
        contentType: "application/json",
        success: function (response) {
            hideSpinner();
            const token = response.token;
            setTokenInClient(token);
            $("#divData").show();
        },
        error: function (errorResponse) {
            console.log(errorResponse);
            showErrorToast(errorResponse.responseText);
        }
    });
};


const hideForms = () => {
    $("#divLogin").hide();
    $("#divRegister").hide();
};

$(document).on("click", "#btnLogin", function (e) {
    e.preventDefault();
    const uiLoginForm = $("#frmLogin");
    uiLoginForm.validate();
    if (uiLoginForm.valid()) {
        hideForms();
        showSpinner();

        loginViewModel.email = $("#txtEmailLogin").val();
        loginViewModel.password = $("#txtPasswordLogin").val();
        setTimeout(function () {
            submitLoginForm(loginViewModel);
        }, 1000);
    }
});

$(document).on("click", "#btnRegister", function (e) {
    e.preventDefault();
    const uiRegisterForm = $("#frmRegister");
    uiRegisterForm.validate();
    if (uiRegisterForm.valid()) {
        hideForms();
        showSpinner();
        registerViewModel.email = $("#txtEmailRegister").val();
        registerViewModel.password = $("#txtPasswordRegister").val();
        registerViewModel.username = $("#txtUsernameRegister").val();
        registerViewModel.displayName = $("#txtDisplayNameRegister").val();
        setTimeout(function () {
            submitRegisterForm(registerViewModel);
        }, 1000);
    }
});

$(document).on("click", "#btnGetCompanies", function () {
    $("#divData").hide();
    showSpinner();

    setTimeout(function () {
        getCompanyData();
    }, 1000);
});

$(document).on("click", "#btnGetById", function () {
    $("#txtCompanyIsin").val('');
    const companyId = $("#txtCompanyId").val();
    if (companyId === null || companyId === undefined || companyId.length === 0) {
        showErrorToast("Please provide a (valid) number");
        return;
    };
    $("#divData").hide();
    showSpinner();

    setTimeout(function () {
        getCompanyDataById(companyId);
    }, 1000);
});

$(document).on("click", "#btnGetByIsin", function (e) {
    $("#txtCompanyId").val('');
    const companyId = $("#txtCompanyIsin").val();
    if (companyId === null || companyId === undefined || companyId.length === 0) {
        showErrorToast("Please provide a (valid) ISIN");
        return;
    };
    $("#divData").hide();
    showSpinner();

    setTimeout(function () {
        getCompanyDataByIsin(companyId);
    }, 1000);
});

$(document).on("click", "#btnLogout", function (e) {
    $("#divData").hide();
    showSpinner();
    window.localStorage.removeItem("jwtToken");
    reloadPage();
});

function editCompany(data) {
    viewModel.companyToEdit(data);
    $("#modal-company-edit").modal("show");
};

function saveEdit(data) {
    $("#edit-company-form").validate();
    if (!$("#edit-company-form").valid()) return;

    $("#modal-company-edit").modal("hide");
    showSpinner();
    setTimeout(function () {
        editCompanyData(data);
    }, 1000);
};

function createCompany() {
    const defaultParamsForNewCompanyCreation = {
        name: '',
        exchange: '',
        ticker: '',
        isin: '',
        website: ''
    };
    viewModel.companyToCreate(defaultParamsForNewCompanyCreation);
    $("#modal-company-create").modal("show");
};


function saveCreate(data) {
    $("#create-company-form").validate();
    if (!$("#create-company-form").valid()) return;

    $("#modal-company-create").modal("hide");
    showSpinner();
    setTimeout(function () {
        createCompanyData(data);
    }, 1000);
};

$(document).ready(function () {
    const userLoggedIn = window.localStorage.getItem("jwtToken") !== null;
    isUserLoggedIn = userLoggedIn;

    const uiDivAccount = $("#divAccount");
    const uiDivData = $("#divData");
    if (!isUserLoggedIn) {
        uiDivAccount.show();
        uiDivData.hide();
    } else {
        uiDivAccount.hide();
        uiDivData.show();
    }

    viewModel = {
        companyData: ko.observableArray([]),
        editCompany: editCompany,
        companyToEdit: ko.observable(),
        saveEdit: saveEdit,
        createCompany: createCompany,
        companyToCreate: ko.observable(),
        saveCreate: saveCreate
    };

    ko.applyBindings(viewModel, document.getElementById('divData'));
});