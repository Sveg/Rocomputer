import { IUser } from "./IUser";
import axios, { AxiosError, AxiosResponse, AxiosRequestConfig } from "../../node_modules/axios/index";
import { IData } from "./IData";

console.log('Load Google login');

//Use email from obj in ShowAllStats instead of input useremail
let user: IUser = {
    email: '',
    firstname: '',
    lastname: '',
    id: -1,
    token: ''
}

addGoogleSignin();

function addGoogleSignin() {
    //<div id="my-signin2"></div> <-- add to html
    gapi.signin2.render('my-signin2', {
        'scope': 'profile email',
        'width': 241,
        'height': 50,
        'longtitle': true,
        'theme': 'light',
      'onsuccess': param => {
        console.log('onsuccess');
        onSignIn(param)
        }
      });
}

function onSignIn(googleUser: any) {
    console.log('On sign in');
    var basic = googleUser.getBasicProfile();
    var auth = googleUser.getAuthResponse();

    user.id = googleUser.getId();
    user.firstname = basic.ofa;
    user.email = basic.U3;
    user.lastname= basic.wea;
    user.token = auth.id_token;
    var obj = {
      Email: user.email,
      Fornavn: user.firstname,
      Efternavn: user.lastname
    }
    login(obj);
    //call to api with obj
    
};
  
function login(obj: any) {
    console.log(obj);
    console.log('Login');
    var url = 'https://restfullapirocomputer.azurewebsites.net/api/RoComputer';
    axios.post<IData>(url, obj)
      .then(function (Response: AxiosResponse<IData>): void {
        console.log(Response.status);
      })
      .catch(function (error: AxiosError): void {
        console.log(error);
      });
}