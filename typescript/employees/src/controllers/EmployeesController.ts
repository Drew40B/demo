import { Employees } from "../dataAccess";
import { Request, Response } from "express";
import { Employee } from "../model";


export class EmployeesController {
    private _employees: Employees;

    constructor(employees: Employees) {
        this._employees = employees;
    }

    public async list(req: Request, res: Response) {

        const result = await this._employees.list();

        res.status(200).json(result);
    }

    public async getById(req: Request, res: Response) {

        const employeeId: number = Number.parseInt(req.params.employeeId);

        const result = await this._employees.findById(employeeId);

        if (result) {
            res.status(200).json(result);
        } else {
            res.sendStatus(404);
        }

    }

    public async create(req: Request, res: Response) {

        const employee: Employee = req.body;

        const result = await this._employees.create(employee);

        res.status(201).json(result);

    }

}