const mysql = require('mysql2');

const pool = mysql.createPool({
    host: process.env.SQL_HOST,
    user: 'root',
    password: process.env.SQL_PASSWORD,
    database: process.env.SQL_DB,
    connectionLimit: 10,
    connectTimeout: 5000
});

console.log("Connected to the Database..");

module.exports = pool;