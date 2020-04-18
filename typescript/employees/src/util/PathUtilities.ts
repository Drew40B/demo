import path from "path";
import fs from "fs";

export class PathUtilities {

    static readonly ROOT_FOLDER_NAME = "rootCommon";

    /**
     * Finds the rootCommon folder by walking the directoy tree
     * @param startFolder option folder to start at. Default is the folder where this file is located
     */
    public static async findRootCommon(startFolder: string = __dirname): Promise<string> {

        let currentPath = startFolder;
        const root = path.parse(startFolder).root;

        // Walk up the directory tree until we find the rootCommon folder
        while (currentPath !== root) {
            const dirs = await fs.promises.readdir(currentPath, { withFileTypes: true });

            const found = dirs.find(d => d.isDirectory && d.name === PathUtilities.ROOT_FOLDER_NAME);

            if (found) {
                return path.join(currentPath, found.name);
            }

            // curent path is now parent
            currentPath = path.dirname(currentPath);

        }

        // Not found
        return null;
    }
}