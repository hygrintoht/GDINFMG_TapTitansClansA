// note: original
const express = require("express");
const college = require("../db/models/college.js");
const router = express.Router();

// This is to inform the express router that the json format will be the format used 
// for the request body. Without this, req.body will cannot be parsed and will always return `undefined`!
router.use(express.json());

router.use((req, res, next) => {
    console.log("Request received.");
    next()
});

router.get("/", function (req, res) {
    res.send("<h1> Hello! </h1>");
});

router.get("/college", (req, res) => {
    if (req.query.limit !== undefined) {
        // has query params (e.g., localhost:3000/college?limit=5)
        console.log("request has query params");
        college.getCollegesWithLimit(req.query.limit, (err, results) => {
            if (err) {
                console.error(err);
                res.status(500);
                res.send("An error has occurred: " + err);
                return;
            }
            res.json(results);
        });
    }
    else {
        // No params (e.g., localhost:3000/college)
        college.getColleges((err, results) => {
            if (err) {
                console.error(err);
                res.status(500);
                res.send("An error has occurred: " + err);
                return;
            }
            res.json(results);
        });
    }
});

router.post("/college", (req, res) => {
    // This is known as `object destructuring`, which allows you to retrieve the respective properties from an object.
    const {name, city, enrollment} = req.body;  

    const newCollege = new college.College(name, city, enrollment);
    college.createCollege(newCollege, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});

router.patch("/college", (req, res) => {
    const {name, updateVals} = req.body;
    college.updateCollege(name, updateVals, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500)
            res.json({message: err});
            return;
        }
        res.json(results);
    });
});

router.delete("/college", (req, res) => {
    const name = req.body.name;
    college.deleteCollege(name, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});

module.exports = router;