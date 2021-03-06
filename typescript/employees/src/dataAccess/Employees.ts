import { PathUtilities } from "../util/PathUtilities";
import path from "path";
import fs from "fs";
import { WriteResult } from "./WriteResult";

import { EmployeeModel } from "../model";



/**
 * In memory representation of an employee database
 */
export class Employees {

    private static _instance: Employees;
    private _database: Map<number, EmployeeModel>;

    constructor() {
        this._database = new Map<number, EmployeeModel>();
    }

    public static async instance(): Promise<Employees> {

        if (!Employees._instance) {
            Employees._instance = await Employees.load();
        }

        return Employees._instance;

    }

    public static async reset() {
        Employees._instance = await Employees.load();
    }


    public static async load(employeesFolder?: string): Promise<Employees> {

        if (!employeesFolder) {
            const folder = await PathUtilities.findRootCommon();

            if (!folder) {
                throw new Error("Unable to find rootCommon folder");
            }

            employeesFolder = path.join(folder, "employees");
        }

        if (!fs.existsSync(employeesFolder)) {
            throw new Error(`Folder does not exist: ${employeesFolder}`);
        }


        const employees = new Employees();

        // Load all the employee files from the employees directory
        const files = await fs.promises.readdir(employeesFolder);

        for (const file of files) {
            const filePath = path.join(employeesFolder, file);
            const contents = await fs.promises.readFile(filePath);
            const employee: EmployeeModel = JSON.parse(contents.toString());

            employees._database.set(employee.id, employee);
        }


        return employees;

    }

    /**
     * List all employees
     */
    public async list(): Promise<EmployeeModel[]> {

        const result: EmployeeModel[] = [];

        this._database.forEach(e => result.push(e));

        return result;
    }

    /**
     * Finds an employee by Id. If not found returns null
      */
    public async findById(employeeId: number): Promise<EmployeeModel | null> {

        const found = this._database.get(employeeId);
        return found ? found : null;
    }

    /**
     * Persist the employee. id property will be assigned
     * @param employee  employee to be save
     * @returns newly created employee with it's corresponding id set.
     */
    public async create(employee: EmployeeModel): Promise<EmployeeModel> {

        let id = this._database.size;

        // ensure the id is available.
        // In a real database scenario we would allow the database to assign the id.
        while (this._database.get(id)) {
            id++;
        }

        employee.id = id;

        this._database.set(id, employee);

        return employee;
    }

    public async update(employee: EmployeeModel): Promise<{ result: WriteResult; employee: EmployeeModel }> {

        const found = await this.findById(employee.id);

        if (!found) {
            return { result: WriteResult.notFound, employee: null };
        }

        this._database.set(employee.id, employee);

        return { result: WriteResult.success, employee: employee };

    }
}