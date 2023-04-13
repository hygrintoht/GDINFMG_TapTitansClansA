// @ts-check
// note: changed
const express = require("express");
//const college = require("../db/models/college.js");

const chat = require("../db/models/chat.js");
const clan_leaders = require("../db/models/clan_leaders.js");
const clan_raid_events = require("../db/models/clan_raid_events.js");
const clan = require("../db/models/clan.js");
const player = require("../db/models/player.js");
const raid_score = require("../db/models/raid_score.js");

// Section: initialize express
// This is to inform the express router that the json format will be the format used 
// for the request body. Without this, req.body will cannot be parsed and will always return `undefined`!
const router = express.Router();
router.use(express.json());

router.use((req, res, next) => {
    console.log("Request received.");
    next()
});

router.get("/", function (req, res) {
    res.send("<h1> Hello! </h1>");
});

//#region chat
// create
    // create chat
router.post("/chat", (req, res) => {

    const {clanID, playerID, message, messageDateTime, messageType} = req.body;
    const newChat = new chat.Chat(clanID, playerID, message, messageDateTime, messageType);

    chat.createChat(newChat, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// read
    // get chat clan messages
router.get("/chat", (req, res) => {

    const chatID = req.body.chatID;

    chat.getChatClanMessages(chatID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// update (not needed)
// delete
    // delete chat
router.delete("/chat", (req, res) => {

    const chatID = req.body.chatID;

    chat.deleteChat(chatID, (err, results) =>{
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
//#endregion

//#region clan
// create
    // create clan
router.post("/clan", (req, res) => {

    const name = req.body.name;
    
    clan.createClan(name, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});
// read (later)

// update
    // update clan
router.patch("/clan", (req, res) => {
    const {clanID, updateVals} = req.body;
    clan.updateClan(clanID, updateVals, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500)
            res.json({message: err});
            return;
        }
        res.json(results);
    });
});
// delete
    // delete clan
router.delete("/clan", (req, res) => {

    const clanID = req.body.clanID;

    clan.deleteClan(clanID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
//#endregion

//#region clan_leaders
// create
    // create clan leaders
router.post("/clan_leaders", (req, res) => {

    const {clanID, playerID, is_grandmaster} = req.body;
    const newClan_Leaders = new clan_leaders.Clan_Leaders(clanID, playerID, is_grandmaster);

    clan_leaders.createClan_Leaders(newClan_Leaders, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// read
    // get clan leaders
router.get("/clan_leaders", (req, res) => {

    const clanID = req.body.clanID;

    clan_leaders.getClan_LeadersInClan(clanID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// update (not needed)
// delete
router.delete("/clan_leaders", (req, res) => {

    const playerID = req.body.playerID;

    clan_leaders.deleteClan_Leaders(playerID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
//#endregion

//#region clan_raid_events
// create
router.post("/clan_raid_events", (req, res) => {

    const {titan_name, clanID} = req.body;
    const newClan_Raid_Events = new clan_raid_events.Clan_Raid_Events(titan_name, clanID);

    clan_raid_events.createClan_Raid_Event(newClan_Raid_Events, (err,results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});
// read (later)

// update (not needed)
// delete
router.delete("/clan_raid_events", (req, res) => {

    const raidID = req.body.raidID;
    
    clan_raid_events.deleteClan_Raid_Events(raidID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});
//#endregion

//#region player
// create
router.post("/player", (req, res) => {

    const {username, email, country, title, installation_date} = req.body;
    const newPlayer = new player.Player(username, email, country, title, installation_date);

    player.createPlayer(newPlayer, (err,results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// read

router.get("/player", (req, res) => {

        player.getPlayer((err, results) => {
            if (err) {
                console.error(err);
                res.status(500);
                res.send("An error has occurred: " + err);
                return;
            }
            res.json(results);
        });
    
});

router.get("/player/:username", (req, res) => {

    const name = req.params.username;

    player.getPlayerStats(name, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
    
});

router.get("/clan/:clanID/players", (req, res) => {

    const clanID = req.params.clanID;

    player.getPlayersFromClan(clanID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
    
});


// update
router.patch("/player", (req, res) => {

    const {playerID, updateVals} = req.body;

    console.log(playerID);
    console.log(req.body.playerID);
    console.log(req.body.updateVals);

    player.updatePlayer(playerID, updateVals, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500)
            res.json({message: err});
            return;
        }
        res.json(results);
    });
});

// delete
router.delete("/player", (req, res) => {

    const playerID = req.body.playerID;

    player.deletePlayer(playerID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
//#endregion

//#region raid_score
// create
router.post("/raid_score", (req, res) => {

    const {raidId, playerID, attacks, damage} = req.body;
    const newRaid_Score = new raid_score.Raid_Score(raidId, playerID, attacks, damage);

    raid_score.createRaid_Score(newRaid_Score, (err,results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// read
router.get("/raid_score", (req, res) => {

    const raidID = req.body.raidID;

    raid_score.getRaid_EventRaid_Score(raidID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});
// update (not needed)
// delete (not needed)
//#endregion

module.exports = router;