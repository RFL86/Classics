import { Component, OnInit } from "@angular/core"
import { ChartService } from "./chart.service";
import { alertReadingStatusChart, productStatusChart, carsByBrand, productByState, usersStatus } from './data';
import { ChartType } from './chart.model';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';



@Component({
  selector: "chart",
  templateUrl: "./chart.html",
  styleUrls: ['./chart.scss'],
  providers: [ChartService]
})

export class ChartComponent implements OnInit {

  productStatusChart: ChartType;
  alertReadingStatusChart: ChartType;
  carsByBrand: ChartType;
  productByState: ChartType;
  usersStatus: ChartType;
  public loaderData: any;


  constructor(public _service: ChartService,) {

  }

  ngOnInit(): void {
    this.loaderData = { visible: true, text: "" };
    this.productStatusChart = productStatusChart;
    this.alertReadingStatusChart = alertReadingStatusChart;
    this.carsByBrand = carsByBrand;
    this.productByState = productByState;
    this.usersStatus = usersStatus;
    this.loaderData = { visible: false, text: "" };
  }

  generatePDF() {
    this.loaderData = { visible: true, text: "" };
    const data = document.getElementById('contentToConvert');
    console.log(data);
    html2canvas(data).then(canvas => {
      const imgWidth = 208;
      const imgHeight = canvas.height * imgWidth / canvas.width;
      const contentDataURL = canvas.toDataURL('image/png')
      let pdf = new jsPDF('p', 'mm', 'a4');
      const position = 0;
      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
      pdf.save('charts.pdf');
      this.loaderData = { visible: false, text: "" };
    });
  }

}


