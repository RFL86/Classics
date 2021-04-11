import { Component, OnInit } from "@angular/core"
import { CarService } from './car.service';
import { SelectItem } from '../../pages.model';
import { CarModalComponent } from './car-modal/car-modal.component';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { LoaderComponent } from '../../../../app/shared/ui/loader/loader.component';



@Component({
  selector: "car",
  templateUrl: "./car.html",
  styleUrls: ['./car.scss'],
  providers: [CarService]
})

export class CarComponent implements OnInit {

  public breadCrumbItems: Array<{}>;
  service: CarService;
  public activeTab: string;
  public newBrand: string;
  public newModel: string;
  public newSerie: string;
  public brandId: string;
  public modelId: string;
  public brandsTable: any;
  public modelsTable: any;
  public seriesTable: any;
  public brandsList: any; 
  public modelsList: any; 
  public selectedBrand: SelectItem[];
  public selectedModel: SelectItem[];
  formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;

  constructor(public _service: CarService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Cadastros Gerais', path: '/' }, { label: 'Modelos de Carro', path: '/', active: true }];
    this.loaderData = { visible: false, text: "" };
    this.activeTab = 'brands';
    this.formData = new FormData();
    this.cleanInputs();
    this.getBrandsTable();
  }


  cleanInputs() {
    this.newBrand = '';
    this.newModel = '';
    this.newSerie = '';
    this.successMessage = '';
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
    this.modelId = '';
    this.brandId = '';
  }

  loadTables() {
    this.getBrandsTable();
    this.getModelsTable();
    this.getSeriesTable();
    this.cleanInputs();
    this.getBrands();
  }

  showEditBrandModal(brandId, status, name) {
    const modal = this.modalService.open(CarModalComponent, { size: 'md' });
    modal.componentInstance.idInput = brandId;
    modal.componentInstance.typeInput = 'Marca';
    modal.componentInstance.statusInput = status;
    modal.componentInstance.nameInput = name;
    modal.result.then(() => { this.loadTables(); }, () => { console.log('Backdrop click') });
  }

  showEditModelModal(modalId, status, name) {
    const modal = this.modalService.open(CarModalComponent, { size: 'md' });
    modal.componentInstance.idInput = modalId;
    modal.componentInstance.typeInput = 'Modelo';
    modal.componentInstance.statusInput = status;
    modal.componentInstance.nameInput = name;
    modal.result.then(() => { this.loadTables(); }, () => { console.log('Backdrop click') });
  }

  showEditSerieModal(serieId, status, name) {
    const modal = this.modalService.open(CarModalComponent, { size: 'md' });
    modal.componentInstance.idInput = serieId;
    modal.componentInstance.typeInput = 'Série';
    modal.componentInstance.statusInput = status;
    modal.componentInstance.nameInput = name;
    modal.result.then(() => { this.loadTables(); }, () => { console.log('Backdrop click') });
  }


  changeTab(tabName: string) {
    this.activeTab = tabName;

    switch (tabName) {
      case 'brands':
        this.getBrandsTable();
        this.cleanInputs();
        break;
      case 'models':
        this.getModelsTable();
        this.cleanInputs();
        this.getBrands();
        break;
      case 'series':
        this.getSeriesTable();
        this.cleanInputs();
        this.getBrands();
        break;      
    }
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

  onSelectBrand(brandId) {
    this.brandId = brandId;
  }

  onSelectModel(modelId) {
    this.modelId = modelId;
  }

  getBrandsTable() {
    this.loaderData = { visible: true, text: "" };
    this._service.getBrandsTable().subscribe(
      result => {
        this.brandsTable = result;
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );
  }

  getModelsTable() {
    this.loaderData = { visible: true, text: "" };

    this._service.getModelsTable().subscribe(
      result => {
        this.modelsTable = result;
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );   
  }

  getSeriesTable() {
    this.loaderData = { visible: true, text: "" };
    this._service.getSeriesTable().subscribe(
      result => {
        this.seriesTable = result;
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );
  }

  addBrand() {
    this.resetFormData();
    this.formData.append('Name', this.newBrand);
    this._service.addBrand(this.formData).subscribe(
      result => {
        if (result == null || result == '') {
          this.newBrand = '';
          this.showSuccessMessage = true;
          this.successMessage = 'Marca cadastrada com sucesso.';
          setTimeout(() => {
            this.successMessage = '';
            this.showSuccessMessage = false;
          },
            2000);
          this.getBrandsTable();
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
    this.resetFormData();
    this.showErrorMessage = false;
    this.showSuccessMessage = false;    
  }

  addModel() {
    if (this.brandId == null || this.brandId == '') {
      this.showErrorMessage = true;
      this.errorMessage = 'Informe uma marca.';
      setTimeout(() => {
        this.errorMessage = '';
        this.showErrorMessage = false;
      },
        2000);
    }
    else {
      this.resetFormData();
      this.formData.append('Name', this.newModel);
      this.formData.append('BrandId', this.brandId);
      this._service.addModel(this.formData).subscribe(
        result => {
          if (result == null || result == '') {
            this.newModel = '';
            this.showSuccessMessage = true;
            this.successMessage = 'Modelo cadastrado com sucesso.';
            setTimeout(() => {
              this.successMessage = '';
              this.showSuccessMessage = false;
            },
              2000);
            this.getModelsTable();
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
      this.resetFormData();
      this.showErrorMessage = false;
      this.showSuccessMessage = false;    
    }   
  }

  addSerie() {
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
      this.resetFormData();
      this.formData.append('Name', this.newSerie);
      this.formData.append('CarModelId', this.modelId);
      this._service.addSerie(this.formData).subscribe(
        result => {
          if (result == null || result == '') {
            this.newSerie = '';
            this.showSuccessMessage = true;
            this.successMessage = 'Série cadastrada com sucesso.';
            setTimeout(() => {
              this.successMessage = '';
              this.showSuccessMessage = false;
            },
              2000);
            this.getSeriesTable();
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
      this.resetFormData();
      this.showErrorMessage = false;
      this.showSuccessMessage = false;
    }
  }

  resetFormData() {
    this.formData.delete('Name');
    this.formData.delete('BrandId');
    this.formData.delete('CarModelId');
  }

}
