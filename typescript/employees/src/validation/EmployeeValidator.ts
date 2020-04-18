
import Ajv from "ajv";
import { EmployeeModel, ErrorModel } from "../model";
import { ValidationResult } from "../validation/ValidationResult";
import { Request, Response, NextFunction } from "express";


export class EmployeeValidator {

    public schema = {
        "$schema": "http://json-schema.org/draft-07/schema",
        "type": "object",
        "title": "Employee Schema",
        "required": [
            "names",
            "role",
            "supervisorId"
        ],
        "properties": {
            "id": {
                "type": "integer",
                "description": "Unique identifier of an employee",
            },
            "names": {
                "type": "array",
                "description": "An array of names",
                "items": {
                    "type": "string"
                }
            },
            "role": {
                "type": "string",
            },
            "supervisorId": {
                "type": "integer",
                "description": "Id of the supervisor",
            }
        }
    }

    /**
     * Express handler that validates and parses the request body. If parse passes req.body is set to the parsed employee
     * @param req  Express Request object
     * @param res  Express Response object
     * @param next next function to call upon success,
     */
    public handle(req: Request, res: Response, next: NextFunction) {

        const parseResult = this.parseAndValidate(req.body);

        if (!parseResult.success) {
            const error: ErrorModel = {
                message: "Error during validation",
                errors: parseResult.errors,
            };

            res.status(400).json(error);
            return;
        }

        if (req.method === "PUT" && parseInt(req.params.employeeId) !== parseResult.object.id) {
            const error: ErrorModel = {
                message: `Conflict: Employeeid does not match ${req.params.employeeId} / ${parseResult.object.id} (path / body)`

            };
            res.status(409).json(error);
            return;
        }

        req.body = parseResult.object;


        next();

    }

    public parseAndValidate(object: unknown): ValidationResult<EmployeeModel> {

        const ajv = new Ajv(); // options can be passed, e.g. {allErrors: true}
        const validate = ajv.compile(this.schema);

        const result = new ValidationResult<EmployeeModel>();

        if (!validate(object)) {

            result.success = false;
            result.errors = [];

            for (const error of validate.errors) {
                result.errors.push(error.message);
            }

            return result;

        }

        result.object = object as EmployeeModel;
        result.success = true;

        // TODO add custom validation here
        return result;

    }
}