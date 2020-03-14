import path from "path";
import fs from "fs";

export class PathUtilities {

    static readonly ROOT_FOLDER_NAME = "rootCommon";

    public static async findRootCommon(startFolder: string = __dirname): Promise<string> {

        let currentPath = startFolder;
        const root = path.parse(startFolder).root;

        while (currentPath !== root) {
            const dirs = await fs.promises.readdir(currentPath, { withFileTypes: true });

            const found = dirs.find(d => d.isDirectory && d.name === PathUtilities.ROOT_FOLDER_NAME);

            if (found) {
                return path.join(currentPath, found.name);
            }

            // curent path is now parent
            currentPath = path.dirname(currentPath);

        }

        return null;
    }
}