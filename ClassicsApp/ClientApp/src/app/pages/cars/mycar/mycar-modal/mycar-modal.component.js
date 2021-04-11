"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var update_recharge_item_modal_service_1 = require("./update-recharge-item-modal.service");
var UpdateRechargeItemModal = /** @class */ (function () {
    function UpdateRechargeItemModal(_service, _activeModal, _http) {
        this._service = _service;
        this._activeModal = _activeModal;
        this._http = _http;
    }
    UpdateRechargeItemModal.prototype.ngAfterViewInit = function () {
    };
    UpdateRechargeItemModal.prototype.ngOnInit = function () {
        this.loadData();
    };
    UpdateRechargeItemModal.prototype.loadData = function () {
        console.log(this.rechargeItemId);
        console.log(this.totalValue);
        console.log(this.dailyValue);
        console.log(this.workDays);
        console.log(this.faresPerDay);
        //this.totalValue = '0,00';
        //this.dailyValue = '0,00';
        this.message = '';
    };
    UpdateRechargeItemModal.prototype.close = function () {
        this._activeModal.close();
    };
    UpdateRechargeItemModal.prototype.calcTotalValue = function () {
        if (this.workDays != '' && this.totalValue != '') {
            var dailyValue = (Number(this.dailyValue));
            var days = (Number(this.workDays));
            var faresPerDay = Number(this.faresPerDay);
            if (days > 0 && dailyValue > 0) {
                var totalValue = (dailyValue * days * faresPerDay);
                this.totalValue = String(totalValue);
                this.message = '';
            }
            else if (!(days > 0)) {
                this.message = 'A quantidade de dias deve ser maior que 0.';
                this.totalValue = '0,00';
                this.alertType = 'warning';
            }
            else if (!(dailyValue > 0)) {
                this.message = 'O valor diário deve ser maior que 0.';
                this.totalValue = '0,00';
                this.alertType = 'warning';
            }
            else {
                this.message = '';
            }
        }
    };
    UpdateRechargeItemModal.prototype.updateRechargeItem = function () {
        var _this = this;
        var formData = new FormData();
        formData.append("RechargeItemId", this.rechargeItemId);
        formData.append("RequestedValue", this.totalValue);
        formData.append("DailyValue", this.dailyValue);
        formData.append("WorkDays", this.workDays);
        this._service.updateRechargeItem(formData).subscribe(function (data) {
            if (data) {
                _this.message = 'Item editado com sucesso.';
                setTimeout(function () {
                    _this.message = '';
                }, 2000);
            }
            else {
                _this.message = 'Ocorreu um erro ao editar o item.';
                setTimeout(function () {
                    _this.message = '';
                }, 2000);
            }
        });
    };
    __decorate([
        core_1.Input()
    ], UpdateRechargeItemModal.prototype, "rechargeItemId", void 0);
    UpdateRechargeItemModal = __decorate([
        core_1.Injectable({
            providedIn: "root"
        }),
        core_1.Component({
            selector: "addrechargeitemmodal",
            templateUrl: "./update-recharge-item-modal.html",
            styleUrls: ['./update-recharge-item-modal.scss'],
            providers: [update_recharge_item_modal_service_1.UpdateRechargeItemModalService, common_1.DecimalPipe]
        })
    ], UpdateRechargeItemModal);
    return UpdateRechargeItemModal;
}());
exports.UpdateRechargeItemModal = UpdateRechargeItemModal;
//# sourceMappingURL=update-recharge-item-modal.component.js.map