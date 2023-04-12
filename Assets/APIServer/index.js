// do not change
require("dotenv").config();
const express = require("express");
const routes = require('./src/routes/routes.js');

const app = express();

app.use(routes);

app.listen(process.env.PORT, function() {
    console.log("Server is now listening...");
});