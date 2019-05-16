import axios, { AxiosError, AxiosResponse, AxiosRequestConfig } from "../../node_modules/axios/index"
import { IData } from "./IData";
import { IUser } from "./User";
const uri: string = "https://restfullapirocomputer20190513120657.azurewebsites.net/api/RoComputer/";

//     let element: HTMLDivElement = <HTMLDivElement>document.getElementById("content");
// let GetStats: HTMLButtonElement = <HTMLButtonElement>document.getElementById("get");
// GetStats.addEventListener("click", getstats);

let ContentElement : HTMLDivElement = <HTMLDivElement> document.getElementById("content");
// let GetAllStats: HTMLButtonElement = <HTMLButtonElement>document.getElementById("getAll");
// GetAllStats.addEventListener("click", ShowAllStats);



function CreateLiElement(text:string, classAttribute:string, id:number) : HTMLLIElement{
    let newLiElement = document.createElement("li");
    let NewText = document.createTextNode(text);

    newLiElement.setAttribute("class", classAttribute);
    newLiElement.setAttribute("id", id.toString());

    newLiElement.appendChild(NewText);

    return newLiElement;
}

function ShowAllStats() : void {
    let test = (<HTMLParagraphElement>document.getElementById("useremail"))
    let id: string = (<HTMLInputElement>document.getElementById("input")).value
    axios.get<IData[]>(uri + test)

    .then(function(response:AxiosResponse<IData[]>):void{
        let ulElement : HTMLUListElement = document.createElement("ul");
        
        let x: number = 0;

        response.data.forEach((data:IData) => {
            x++;
            if (data == null)
            {
                ulElement.appendChild(CreateLiElement("NULL element","error",x));
            }
            else
            {
                let tekst : string = `ID på Turen: ${data.id} Hastighed: ${data.hastighed} Acceleration: ${data.acceleration} Tid: ${data.tid} Email: ${data.fkEmail}`;
                ulElement.appendChild(CreateLiElement(tekst,"r1",data.id))
            }
        });

        if (ContentElement.firstChild) 
        
            ContentElement.removeChild(ContentElement.firstElementChild);
            ContentElement.appendChild(ulElement);
        }
        )
        .catch(function (error:AxiosError):void{
            ContentElement.innerHTML = error.message;
        })
        
}


function getstats(): void{

    let id: string = (<HTMLInputElement>document.getElementById("input")).value
    axios.get<IData[]>(uri + id)

        .then(function (Response: AxiosResponse<IData[]>): void{
            console.log(Response);
            let result = ``;
            Response.data.forEach((Data: IData) => {
                result += ``;
                
                
                
            });
        })
        .catch(function (error: AxiosError): void{
        console.log(error)
    })
}


// aadsadsadasdasdasdasdasdasdasdasdasdadas





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