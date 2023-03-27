function SaveToken(name, token) {
    localStorage.setItem(name, token);
}

function GetToken(name) {

    return localStorage.getItem(name);
  
}

function RemoveToken(name) {
    localStorage.removeItem(name);
}
