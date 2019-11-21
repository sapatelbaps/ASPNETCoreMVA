var express = require("express");
var router = express.Router();
const https = require("https");

/* GET home page. */
router.get("/Quote", function(req, res, next) {
  //https://jsonplaceholder.typicode.com/todos

  //return res.status(200).json({'SwamiShriji' : 'BAPS'});
  const options = {
    hostname: "jsonplaceholder.typicode.com",
    //port: 443,
    path: "/todos",
    method: "GET"
  };

  const req1 = https.request(options, res1 => {
    console.log(`statusCode: ${res1.statusCode}`);
    let temp = '';
    res1.on("data", d => {
      temp += d;
    });
    res1.on("end", () => {
      return res.status(200).json(temp);
    });
  });

  req1.on("error", error => {
    console.error(error);
  });

  req1.end();
});

router.get("/OrderEstimate", function(req, res, next) {});

router.get("/FeatureAPIMethod", function(req, res, next) {});

module.exports = router;
