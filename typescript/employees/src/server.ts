import app from "./app";

const PORT = app.get("port") || 3000;

app.listen(PORT, () => console.log(`Listening on port ${PORT}`));