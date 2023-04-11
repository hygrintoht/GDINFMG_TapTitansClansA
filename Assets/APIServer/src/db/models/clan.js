const pool = require("../db.js");

class College {
    constructor (name, city, enrollment) {
        this.name = name;
        this.city = city;
        this.enrollment = enrollment;
    }
}

function getColleges(callback) {
    let stmt = 'SELECT * FROM college';
    pool.query(stmt, [], callback);
}

function getCollegesWithLimit(limit, callback) {
    let stmt = `SELECT * FROM college LIMIT ?`;
    pool.execute(stmt, [limit], callback);
}

function createCollege(college, callback) {
    let stmt = `INSERT INTO college (cName, city, enrollment)
        VALUES (?,?,?)`;
    if (college instanceof College) // not necessary, but can be added to ensure that the college object is created from the College constructor
        pool.execute(stmt, [college.name, college.city, college.enrollment], callback);
}

function updateCollege(name, update_college, callback) {
    let stmt = `UPDATE college SET `;
    let update_fields = []; // store the fields to update in an array, so that the list of fields are in order
    for (const prop in update_college) {
        const setStr = `${prop}=?,`;
        update_fields.push(update_college[prop]);
        stmt += setStr;
    }
    stmt = stmt.substring(0, stmt.length-1); // removes the last semicolon from the string
    stmt += ` WHERE cName=?`;
    update_fields.push(name);
    pool.execute(stmt, update_fields, callback);
}

function deleteCollege(name, callback) {
    let stmt = `DELETE FROM college WHERE cName=?`;
    pool.execute(stmt, [name], callback);
}

module.exports = {
    College,
    getColleges,
    getCollegesWithLimit,
    createCollege,
    updateCollege,
    deleteCollege
};