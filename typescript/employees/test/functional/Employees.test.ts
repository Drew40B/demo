import { Employees } from "../../src/dataAccess/Employees";
import { PathUtilities } from "../../src/util/PathUtilities";
import { pathToFileURL } from "url";
import path from "path";
import { Employee } from "../../src/model";

beforeEach(async () => {
    // Reset employees so tests to bleed into each other
    await Employees.reset();
});

describe("Employees", () => {
    describe("load", () => {

        it("happy path", async (done) => {

            const results = await Employees.load();

            expect(results).not.toBeNull();
            done();
        });
    });

    describe("list", () => {
        it("happy path", async (done) => {

            const employees = await Employees.instance();

            const result = await employees.list();

            expect(result.length).toBeGreaterThan(0);
            done();
        });
    });

    describe("findById", () => {
        it("Happy path", async (done) => {
            const employees = await Employees.instance();
            const result = await employees.findById(1);

            expect(result).not.toBeNull();
            expect(result.id).toBe(1);

            done();
        });

        it("not found", async (done) => {

            const employees = await Employees.instance();
            const result = await employees.findById(-1);

            expect(result).toBeNull();

            done();
        });
    });

    describe("create", () => {

        it("happy path", async (done) => {

            const employees = await Employees.instance();

            const employee: Employee = { 
                names : ["Unit","Test"],
                role: "test",
                id : -1,
                supervisorId : 0
             };

            let result = await employees.create(employee);

            expect(result).toBeDefined();
            expect(result.id).toBeGreaterThanOrEqual(0);

            // Verify saved in database
            result = await employees.findById(result.id);
            expect(result).not.toBeNull();
  
            done();
        });
    });
});