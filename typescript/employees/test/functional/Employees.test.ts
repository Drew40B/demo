import { Employees } from "../../src/dataAccess/Employees";
import { PathUtilities } from "../../src/util/PathUtilities";
import { pathToFileURL } from "url";
import path from "path";

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

        it("Happy path", async (done) => {
            const employees = await Employees.instance();
            const result = await employees.findById(-1);

            expect(result).toBeNull();
         

            done();
        });
    });
});