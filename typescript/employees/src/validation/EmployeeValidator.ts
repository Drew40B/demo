
import Ajv from "ajv";
import { Employee } from "../model";
import { ValidationResult } from "../validation/ValidationResult";

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


    public parseAndValidate(object: unknown): ValidationResult<Employee> {

        const ajv = new Ajv(); // options can be passed, e.g. {allErrors: true}
        const validate = ajv.compile(this.schema);

        const result = new ValidationResult<Employee>();

        if (!validate(object)) {
         
            result.success = false;
            result.errors = [];

            for (const error of validate.errors) {
                result.errors.push(error.message);
            }

            return result;

        }

        result.object = object as Employee;
        result.success = true;

        // TODO add custom validation here
        return result;

    }
}