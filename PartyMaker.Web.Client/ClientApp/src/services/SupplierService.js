﻿import {BaseService} from "./BaseService";

const _baseService = new BaseService();

export class SupplierService {
    insertNewSupplier = data => _baseService.setData("Account/Register", data);
}