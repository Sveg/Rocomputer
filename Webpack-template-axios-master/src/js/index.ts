import axios, { AxiosError, AxiosResponse, AxiosRequestConfig } from "../../node_modules/axios/index"
import { IData } from "./IData";

let element: HTMLDivElement = <HTMLDivElement>document.getElementById("content");
let GetStats: HTMLButtonElement = <HTMLButtonElement>document.getElementById("get");
GetStats.addEventListener("click", getstats)

function getstats(): void{

    let id: string = (<HTMLInputElement>document.getElementById("input")).value
    axios.get<IData[]>("https://restfullapirocomputer20190513120657.azurewebsites.net/api/RoComputer/" + id)

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