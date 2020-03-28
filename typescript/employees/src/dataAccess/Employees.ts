import { PathUtilities } from "../util/PathUtilities";
import path from "path";
import fs from "fs";

import { Employee } from "../model";


/**
 * In memory representation of an employee database
 */
export class Employees {

    private static _instance: Employees;
    private _database: Map<number, Employee>;

    constructor() {
        this._database = new Map<number, Employee>();
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
            const employee: Employee = JSON.parse(contents.toString());

            employees._database.set(employee.id, employee);
        }


        return employees;

    }

    /**
     * List all employees
     */
    public async list(): Promise<Employee[]> {

        const result: Employee[] = [];

        this._database.forEach(e => result.push(e));

        return result;
    }

    /**
     * Finds an employee by Id. If not found returns null
      */
    public async findById(employeeId: number): Promise<Employee | null> {

        const found = this._database.get(employeeId);
        return found ? found : null;
    }
}