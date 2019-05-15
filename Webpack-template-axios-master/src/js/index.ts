import axios, { AxiosError, AxiosResponse, AxiosRequestConfig } from "../../node_modules/axios/index"
import { IData } from "./IData";


const uri: string = "https://restfullapirocomputer20190513120657.azurewebsites.net/api/RoComputer/";

let element: HTMLDivElement = <HTMLDivElement>document.getElementById("content");
let GetStats: HTMLButtonElement = <HTMLButtonElement>document.getElementById("get");
GetStats.addEventListener("click", getstats);

let ContentElement : HTMLDivElement = <HTMLDivElement> document.getElementById("content");
let GetAllStats: HTMLButtonElement = <HTMLButtonElement>document.getElementById("getAll");
GetAllStats.addEventListener("click", ShowAllStats);


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