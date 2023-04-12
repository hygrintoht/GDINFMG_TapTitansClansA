const pool = require("../db.js");

class Chat{
    constructor(clanID, playerID, message, messageDateTime, messageType){
    this.clanID = clanID;
    this.playerID = playerID;
    this.message = message;
    this.messageDateTime = messageDateTime;
    this.messageType = messageType;
    }
}
// create
function createChat(chat, callback){
    let stmt = 'INSERT INTO chat (clanID, playerID, message, messageDateTime, messageType) VALUES (?,?,?,?,?)';
    pool.execute(stmt, [chat.clanID, chat.playerID, chat.message, chat.messageDateTime, chat.messageType], callback);
}
// read
function getChatClanMessages(clanID, callback){
    let stmt = 'SELECT * FROM chat WHERE clanID = ?';
    pool.execute(stmt, [clanID], callback);
}
// update
// delete
function deleteChat(chatID, callback){
    let stmt = 'DELETE FROM chat WHERE chatID = ?';
    pool.execute(stmt, [chatID], callback);
}

module.exports = {
    Chat,
    createChat,
    getChatClanMessages,
    deleteChat
};