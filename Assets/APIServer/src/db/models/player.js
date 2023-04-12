const pool = require("../db.js");

class Player{
    constructor(username, email, country, title, installation_date)
    {
        this.username = username;
        this.email = email;
        this.country = country;
        this.title = title;
        this.installation_date = installation_date;
    }
}
// create
function createPlayer(player, callback)
{
    let stmt = 'INSERT INTO player (username, email, country, title, installation_date) VALUES (?,?,?,?,?)';
    pool.execute(stmt, [player.username, player.email, player.country, player.title, player.installation_date], callback);
}
// read
    // get list of players
function getPlayer(callback){
    let stmt = 'SELECT * FROM player';
    pool.query(stmt, [], callback);
}
    // get limited list of players
function getPlayerWithLimit(limit, callback){
    let stmt = 'SELECT * FROM player LIMIT ?';
    pool.execute(stmt, [limit], callback);
}
    // get player id from username
function getPlayerID(name, callback){
    let stmt = 'SELECT playerID FROM player WHERE username = ?';
    pool.execute(stmt, [name], callback);
}
// update
    // general update function
function updatePlayer(playerID, update_player, callback) {
    let stmt = `UPDATE player SET `;
    // store the fields to update in an array, so that the list of fields are in order
    let update_fields = [];
    for (const prop in update_player) {
        const setStr = `${prop}=?,`;
        update_fields.push(update_player[prop]);
        stmt += setStr;
    }
    // removes the last semicolon from the string
    stmt = stmt.substring(0, stmt.length-1); 
    stmt += ` WHERE playerID = ?`;
    update_fields.push(playerID);
    pool.execute(stmt, update_fields, callback);
}
// delete
function deletePlayer(playerID, callback){
    let stmt = 'DELETE FROM player WHERE playerID = ?';
    pool.execute(stmt, [playerID], callback);
}

module.exports = {
    Player,
    createPlayer,
    getPlayer,
    getPlayerWithLimit,
    getPlayerID,
    updatePlayer,
    deletePlayer
};