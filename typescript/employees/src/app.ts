import express from "express";
import compression from "compression";  // compresses requests
import bodyParser from "body-parser";

// Controllers (route handlers)
import { EmployeesController } from "./controllers/EmployeesController";
import { Employees } from "./dataAccess";

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

    app.get("/", controller.list.bind(controller));
    app.get("/:employeeId", controller.getById.bind(controller));

    app.put("/",controller.update.bind(controller));
    app.post("/",controller.create.bind(controller));
    
    console.log("initialized");
}







