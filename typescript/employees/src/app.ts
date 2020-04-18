import express from "express";
import compression from "compression";  // compresses requests
import bodyParser from "body-parser";

// Controllers (route handlers)
import { EmployeesController } from "./controllers/EmployeesController";
import { Employees } from "./dataAccess";
import { EmployeeValidator } from "./validation/EmployeeValidator";

// Create Express server
export const app = express();

// Express configuration
app.set("port", process.env.PORT || 3000);
app.use(compression());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));


export async function init() {

    console.log("initializing");

    const employees = await Employees.instance();
    const controller = new EmployeesController(employees);
    const employeeValidator = new EmployeeValidator();

    const validator = employeeValidator.handle.bind(employeeValidator);

    const baseUri = "/v1/employees/";

    // Routes 
    app.get(baseUri, controller.list.bind(controller));
    app.get(baseUri + ":employeeId", controller.getById.bind(controller));

    app.put(baseUri + ":employeeId", validator,controller.update.bind(controller));
    app.post(baseUri,validator,validator, controller.create.bind(controller));

    console.log("initialized");
}







