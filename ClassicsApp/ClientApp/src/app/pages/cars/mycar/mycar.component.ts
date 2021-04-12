import { Component, OnInit } from "@angular/core"
import { MyCarService } from './mycar.service';
import { SelectItem } from '../../pages.model';
import { MyCarModalComponent } from './mycar-modal/mycar-modal.component';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { LoaderComponent } from '../../../../app/shared/ui/loader/loader.component';




@Component({
  selector: "mycar",
  templateUrl: "./mycar.html",
  styleUrls: ['./mycar.scss'],
  providers: [MyCarService]
})

export class MyCarComponent implements OnInit {

  public breadCrumbItems: Array<{}>;
  tableData: any;
  service: MyCarService;

  public selectedBrand: SelectItem[];
  public selectedModel: SelectItem[];
  public selectedSerie: SelectItem[];
  public brandsList: any;
  public modelsList: any; 
  public serieList: any;
  public myCarsTable: any;
  public brandId: string;
  public modelId: string;
  public serieId: string;
  public name: string;
  formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;

  

  constructor(public _service: MyCarService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Minha Conta', path: '/' }, { label: 'Meus Carros', path: '/', active: true }];   
    this.loaderData = { visible: false, text: "" };

    this.getMyCarsTable();
    this.getBrands();
    this.formData = new FormData();
    this.cleanInputs();  
  }

  cleanInputs() {
    this.name = '';
    this.serieId = '';
    this.successMessage = '';
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
    this.modelId = '';
    this.brandId = '';
  }

  getMyCarsTable() {
    this.loaderData = { visible: true, text: "" };
    this._service.getMyCarsTable().subscribe(
      result => {
        this.loaderData = { visible: false, text: "" };
        this.myCarsTable = result;
      },
      error => {
        console.log(error);
      }
    );

  }

  onSelectBrand(brandId) {
    this.brandId = brandId;
    this.modelId = '';
    this.serieId = '';
    this.getModelsByBrandId(brandId);
  }

  onSelectModel(modelId) {
    this.modelId = modelId;
    this.serieId = '';
    this.getSeriesByModelId(modelId);
  }

  onSelectSerie(serieId) {
    this.serieId = serieId;
  }

  getBrands() {
    this._service.getBrands().subscribe(
      result => {
        this.selectedBrand = [];
        this.brandsList = null;
        this.brandsList = result;
      },
      error => {
        console.log(error);
      }
    );
  }


  getModelsByBrandId(brandId) {
    this.brandId = brandId;
    this._service.getModelsByBrandId(brandId).subscribe(
      result => {
        this.selectedModel = [];
        this.modelsList = null;
        this.modelsList = result;
      },
      error => {
        console.log(error);
      }
    );
  }

  getSeriesByModelId(modelId) {
    this.modelId = modelId;
    this._service.getSeriesByModelId(modelId).subscribe(
      result => {
        this.selectedSerie = [];
        this.serieList = null;
        this.serieList = result;
      },
      error => {
        console.log(error);
      }
    );
  }

  addCar() {
    if (this.modelId == null || this.modelId == '') {
      this.showErrorMessage = true;
      this.errorMessage = 'Informe um modelo.';
      setTimeout(() => {
        this.errorMessage = '';
        this.showErrorMessage = false;
      },
        2000);
    }
    else {
      this.loaderData = { visible: true, text: "" };
      this.resetFormData();
      this.formData.append('Name', this.name);
      this.formData.append('SerieId', this.serieId);
      this._service.addMyCar(this.formData).subscribe(
        result => {
          if (result == null || result == '') {
            this.name = '';
            this.showSuccessMessage = true;
            this.successMessage = 'Carro cadastrado com sucesso.';
            setTimeout(() => {
              this.successMessage = '';
              this.showSuccessMessage = false;
              this.getMyCarsTable();
            },
              2000);
          } else {
            this.showErrorMessage = true;
            this.errorMessage = result;
            setTimeout(() => {
              this.errorMessage = '';
              this.showErrorMessage = false;
            },
              2000);
          }
        },
        error => {
          console.log(error);
        }
      );
      this.loaderData = { visible: false, text: "" };
      this.resetFormData();
      this.showErrorMessage = false;
      this.showSuccessMessage = false;      
    }
  }

  resetFormData() {
    this.formData.delete('Name');
    this.formData.delete('SerieId');
  }

  showEditMyCarModal(myCarId, name) {
    const modal = this.modalService.open(MyCarModalComponent, { size: 'md' });
    modal.componentInstance.idInput = myCarId;
    modal.componentInstance.nameInput = name;
    modal.result.then(() => { this.getMyCarsTable(); }, () => { console.log('Backdrop click') });
  }

}
