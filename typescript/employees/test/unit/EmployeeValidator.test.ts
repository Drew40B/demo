import { EmployeeValidator } from "../../src/validation/EmployeeValidator";

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
});