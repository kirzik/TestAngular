import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable()


export class TaskDataService {

  constructor(private http: HttpClient) { }
   
  baseUrl = "https://localhost:5001/meters";

  //получить список всех ПУ
  getEmList(){
    return this.http.get<any>(this.baseUrl);
  }
  // получить данные по ПУ
  getData(serialNumber: string, dateTime:string){
    return this.http.get<any>(this.baseUrl + "/" + serialNumber + "/" + dateTime);
  }

}
