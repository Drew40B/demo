import { app, init } from "./app";

init().then(() => {
    const PORT = app.get("port") || 3000;
    app.listen(PORT, () => console.log(`Listening on port ${PORT}`));
}).catch((e) => {
    console.error(e);
    process.exit(-1);
});

