import request from "supertest";
import { app, init } from "../src/app";

beforeAll(async () => {
    await init();
});

describe("GET /", () => {
    it("should return 200 OK", (done) => {
        request(app).get("/")
            .expect(200, done);
    });
});
