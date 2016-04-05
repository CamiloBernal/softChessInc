var isRevertMovement = false;

$(function() {
    var parsePiece = function(piece) {
        var pieceLetter = piece.charAt(1);
        switch (pieceLetter) {
        case "K":
            return "King";
        case "R":
            return "Rook";
        case "P":
            return "Pawn";
        case "N":
            return "Knight";
        case "Q":
            return "Queen";
        case "B":
            return "Bishop";
        };
        return "";
    };

    var parsePosition = function(position) {
        var strPositionX = position.charAt(0);
        var positionX = 0;
        var positionY = parseInt(position.charAt(1), 10) - 1;
        switch (strPositionX) {
        case "a":
            positionX = 0;
            break;
        case "b":
            positionX = 1;
            break;
        case "c":
            positionX = 2;
            break;
        case "d":
            positionX = 3;
            break;
        case "e":
            positionX = 4;
            break;
        case "f":
            positionX = 5;
            break;
        case "g":
            positionX = 6;
            break;
        case "h":
            positionX = 7;
            break;
        }
        return {
            X: positionX,
            Y: positionY
        };
    };
    var parseAvailableMovements = function(movements) {
        var strMovArray = "";

        $.each(movements, function(index, item) {
            strMovArray += "[X: " + item.X + ", Y: " + item.Y + "]" + "\n";
        });
        return strMovArray;
    };

    var showError = function(message) {
        $("#errorMessage").text(message);
        $(".console").removeClass("hidden");
    };

    var hideError = function() {
        $(".console").addClass("hidden");
        $("#errorMessage").text("");
    };
    var calculeMovement = function(oldPos, newPos) {
        var oldInv = [];
        var newInv = [];
        $.each(oldPos, function(key, value) {
            var piece = value;
            var pos = key;
            oldInv.push({
                piece: parsePiece(piece),
                position: parsePosition(pos)
            });
        });
        $.each(newPos, function(key, value) {
            var piece = value;
            var pos = key;
            newInv.push({
                piece: parsePiece(piece),
                position: parsePosition(pos)
            });
        });
        var dataToValidate = {};
        $.each(newInv, function(index, item) {
            var old = $.grep(oldInv, function(itemA, indexA) {
                return itemA.piece == item.piece;
            });

            if (old.length > 0) {
                var itemReal = old[0];
                if (itemReal.position != item.position) {
                    dataToValidate = {
                        PieceType: itemReal.piece,
                        CurrentPosition: itemReal.position,
                        NextPosition: item.position
                    };
                }
            }
        });
        return dataToValidate;
    };

    var onChange = function(oldPos, newPos) {
        if (isRevertMovement) return;
        var dataToValidate = calculeMovement(oldPos, newPos);
        postToApi("Rules", dataToValidate, function(data) {
            console.log("OK");
            hideError();
        }, function(xhr, status, error) {
            if (xhr.status == 403) {
                var msgData = JSON.parse(xhr.responseText);
                var message = msgData.Message + "\n" + "Available positions: " + "\n\t" + parseAvailableMovements(msgData.AvailableMovements);
                showError(message);
                isRevertMovement = true;
                board.position(oldPos);
                isRevertMovement = false;
            } else {
                showError(error);
            }
        });
    };

    var cfg = {
        pieceTheme: "/libs/chessboardjs/img/chesspieces/wikipedia/{piece}.png",
        draggable: true,
        position: {
            a1: "wR",
            b1: "wB"
        },
        showNotation: false,
        onChange: onChange
    };
    var board = ChessBoard("chessBoard", cfg);
});

var postToApi = function(path, data, callBack, failCallback) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(data),
        url: webApiPath + path,
        contentType: "application/json"
    }).done(function(serverData, textStatus, jqXHR) {
        callBack(serverData);
    }).fail(function(xhr, status, error) {
        console.debug("Error enviando datos para el controlador:" + path + " Por favor verifique.");
        console.debug(error);
        if (typeof failCallback == "function") {
            failCallback(xhr, status, error);
        }
    });
};

var callToApi = function(path, data, callBack, failCallback) {
    $.ajax({
        type: "GET",
        data: JSON.stringify(data),
        url: webApiPath + path,
        contentType: "application/json"
    }).done(function(serverData, textStatus, jqXHR) {
        callBack(serverData);
    }).fail(function(xhr, status, error) {
        console.debug("Error enviando datos para el controlador:" + path + " Por favor verifique.");
        console.debug(error);
        if (typeof failCallback == "function") {
            failCallback(xhr, status, error);
        }
    });
};