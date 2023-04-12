const pool = require("../db.js");

// This is how classes are defined in JS. 
// Fields are automatically created if it defined in the constructor.
// See https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Classes for more information.
class College {
    constructor (name, city, enrollment) {
        this.name = name;
        this.city = city;
        this.enrollment = enrollment;
    }
}
// create
function createCollege(college, callback) {
    let stmt = `INSERT INTO college (cName, city, enrollment) VALUES (?,?,?)`;
    // not necessary, but can be added to ensure that the college object is created from the College constructor
    if (college instanceof College) 
        pool.execute(stmt, [college.name, college.city, college.enrollment], callback);
}
// read
function getColleges(callback) {
    let stmt = 'SELECT * FROM college';
    pool.query(stmt, [], callback);
}

function getCollegesWithLimit(limit, callback) {
    let stmt = `SELECT * FROM college LIMIT ?`;
    pool.execute(stmt, [limit], callback);
}
// update
function updateCollege(name, update_college, callback) {
    let stmt = `UPDATE college SET `;
    // store the fields to update in an array, so that the list of fields are in order
    let update_fields = [];
    for (const prop in update_college) {
        const setStr = `${prop}=?,`;
        update_fields.push(update_college[prop]);
        stmt += setStr;
    }
    // removes the last semicolon from the string
    stmt = stmt.substring(0, stmt.length-1); 
    stmt += ` WHERE cName=?`;
    update_fields.push(name);
    pool.execute(stmt, update_fields, callback);
}
// delete
function deleteCollege(name, callback) {
    let stmt = `DELETE FROM college WHERE cName=?`;
    pool.execute(stmt, [name], callback);
}

module.exports = {
    College,
    createCollege,
    getColleges,
    getCollegesWithLimit,
    updateCollege,
    deleteCollege
};