import { Component, OnInit } from '@angular/core';
import { TaskDataService } from '../shared/task-data.service';


@Component({
  selector: 'app-table',
    templateUrl: './table.component.html',
    styleUrls: ['./table.component.css'],
  providers: [TaskDataService]
})
export class TableComponent implements OnInit {

  constructor(
    private getTaskDataService:TaskDataService
  ) { }

  valueDate!:any;
  data:any[] = [];
  emList:any[] = [];
  emValue: string | undefined;
  valueSelect!: string;
  coefficient = 1000; // кило*
  selectedBool = false;

  onChangeDate(value:any){
    console.log(value);
    this.valueDate = value;
  }

  btn(val:number){
    this.valueDate = new Date(new Date(this.valueDate).setDate(new Date(this.valueDate).getDate() + val)).toISOString().substring(0, 10);
  }

  btnUpdate(){
    this.updateEm();
    if(this.valueSelect == undefined)
    {
      this.selectedBool = false;
    }
    else{
      this.selectedBool = true;
    }
  }

  updateEm(){
    this.getTaskDataService.getData(this.valueSelect,this.valueDate).subscribe(x=> { this.data = x});
  }

  ngOnInit(): void {
    console.log("Сегодня: " + this.valueDate);
    this.getTaskDataService.getEmList().subscribe(x=> { this.emList = x});
    this.valueDate = new Date().toISOString().substring(0, 10);
  }
  
}
