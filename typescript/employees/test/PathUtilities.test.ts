
import { PathUtilities } from "../src/utils";
import path from "path";
import fs from "fs";
describe("PathUtilities", () => {
    describe("findRootCommon", () => {

        it("happy path", async (done) => {

           const rootPath = await PathUtilities.findRootCommon();

           expect(rootPath).not.toBeNull();
           expect( fs.existsSync(rootPath)).toBe(true);
           expect(path.basename(rootPath)).toBe(PathUtilities.ROOT_FOLDER_NAME);

        done();

        });

        it("not found", async (done) => {

            const start = path.parse(__dirname).root;
            const rootPath = await PathUtilities.findRootCommon(start);
 
            expect(rootPath).toBeNull();
        
         done();
 
         });
    });

});