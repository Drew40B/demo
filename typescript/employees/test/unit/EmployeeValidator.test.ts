import { EmployeeValidator } from "../../src/validation/EmployeeValidator";
import * as httpMocks from "node-mocks-http";
import { ValidationResult } from "../../src/validation/ValidationResult";
import { EmployeeModel, ErrorModel } from "../../src/model";

describe("EmployeeValidator", () => {


    it("happy path", async (done) => {

        const validator = new EmployeeValidator();

        const obj = {
            names: ["Drew", "Bentley"],
            role: "CIO",
            supervisorId: 0
        };

        const result = validator.parseAndValidate(obj);

        expect(result.success).toBe(true);
        expect(result.object).toBeDefined();
        expect(result.object.names[0]).toBe("Drew");

        done();
    });


    it("invalid object -> missing names", async (done) => {

        const validator = new EmployeeValidator();

        const obj = {

            role: "CIO",
            supervisorId: 0
        };

        const result = validator.parseAndValidate(obj);

        expect(result.success).toBe(false);
        expect(result.object).not.toBeDefined();
        expect(result.errors.length).toBe(1);
        expect(result.errors[0]).toMatch(/.*required.*names.*/);

        done();
    });

    it("handle -> validate returns true", async (done) => {

        const mockRequest = httpMocks.createRequest({
            method: "PUT",
            params: {
                employeeId: 1,
            },

        });

        const mockResponse = httpMocks.createResponse();

        const validator = new EmployeeValidator();

        // Mock out the parse
        validator.parseAndValidate = (obj) => {

            const result = new ValidationResult<EmployeeModel>();
            result.success = true;
            result.object = {
                id: 1,
                names: [],
                role: "",
                supervisorId: -1
            };

            return result;
        };

        let nextCalled = false;
        const next = () => {
            nextCalled = true;
        };

        validator.handle(mockRequest, mockResponse, next);
     
        expect(nextCalled).toBe(true);


        done();
    });

    it("handle -> validate returns false", async (done) => {

        const mockRequest = httpMocks.createRequest({
            method: "POST",
        });

        const mockResponse = httpMocks.createResponse();

        const validator = new EmployeeValidator();

        // Mock out the parse
        validator.parseAndValidate = (obj) => {

            const result = new ValidationResult<EmployeeModel>();
            result.success = false;
            result.errors = ["One", "Two"];

            return result;
        };

        let nextCalled = false;
        const next = () => {
            nextCalled = true;
        };

        validator.handle(mockRequest, mockResponse, next);

        expect(mockResponse.statusCode).toBe(400);

        const result: ErrorModel = mockResponse._getJSONData();
        expect(result.message).toBeDefined();
        expect(result.errors.length).toBe(2);
        expect(result.errors[0]).toBe("One");
        expect(result.errors[1]).toBe("Two");
        expect(nextCalled).toBe(false);


        done();
    });

    it("handle -> conflict Ids differ between URL and body", async (done) => {

        const mockRequest = httpMocks.createRequest({
            method: "PUT",
            params: {
                employeeId: 1,
            },

        });

        const mockResponse = httpMocks.createResponse();

        const validator = new EmployeeValidator();

        // Mock out the parse
        validator.parseAndValidate = (obj) => {

            const result = new ValidationResult<EmployeeModel>();
            result.success = true;
            result.object = {
                id: 2,
                names: [],
                role: "",
                supervisorId: -1
            };

            return result;
        };

        let nextCalled = false;
        const next = () => {
            nextCalled = true;
        };

        validator.handle(mockRequest, mockResponse, next);

        expect(mockResponse.statusCode).toBe(409);

        const result: ErrorModel = mockResponse._getJSONData();
        expect(result.message).toBeDefined();
     
        expect(nextCalled).toBe(false);


        done();
    });
});