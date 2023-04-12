const pool = require("../db.js");

class Clan{
    constructor(name)
    {
        this.name = name;
    }
}
// create
function createClan(clan, callback)
{
    let stmt = 'INSERT INTO clan (clan_name) VALUES (?)';
    pool.execute(stmt, [clan.name], callback);
}
// read
    // get list of clans
function getClans(callback){
    let stmt = 'SELECT * FROM clan';
    pool.query(stmt, [], callback);
}
    // get limited list of clans
function getClansWithLimit(limit, callback){
    let stmt = 'SELECT * FROM clan LIMIT ?';
    pool.execute(stmt, [limit], callback);
}
    // get clan id from name
function getClanID(name, callback){
    let stmt = 'SELECT clanID FROM clan WHERE clan_name = ?';
    pool.execute(stmt, [name], callback);
}
// update
    // general update function
function updateClan(id, update_clan, callback) {
    let stmt = `UPDATE clan SET `;
    // store the fields to update in an array, so that the list of fields are in order
    let update_fields = [];
    for (const prop in update_clan) {
        const setStr = `${prop}=?,`;
        update_fields.push(update_clan[prop]);
        stmt += setStr;
    }
    // removes the last semicolon from the string
    stmt = stmt.substring(0, stmt.length-1); 
    stmt += ` WHERE clanID = ?`;
    update_fields.push(id);
    pool.execute(stmt, update_fields, callback);
}
// delete
function deleteClan(id, callback){
    let stmt = 'DELETE FROM clan WHERE clanID = ?';
    pool.execute(stmt, [id], callback);
}

module.exports = {
    Clan,
    createClan,
    getClans,
    getClansWithLimit,
    getClanID,
    updateClan,
    deleteClan
};