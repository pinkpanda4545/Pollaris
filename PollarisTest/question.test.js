const jsdom = require('jsdom');
const deleteOption = require('../Pollaris/wwwroot/js/question.js');
const { JSDOM } = jsdom;
const dom = new JSDOM('<!DOCTYPE html><html><body></body></html>');
const window = dom.window;
const $ = require('jquery')(window);
const question = require('../Pollaris/wwwroot/js/question.js')

function setup(questionType) {
    var circleHtml = "";
    if (questionType != "R") {
        circleHtml = "<div id='circle-1' class='empty-circle' onclick='redCircle(this)'></div>";
    } else {
        circleHtml = "<div id='circle-1'></div>";
    }

    var deleteBtnHtml = "";
    if (questionType != "TF") {
        deleteBtnHtml = "<div id='bd-1' class='btn-delete' onclick='deleteOption(1)'>" +
            "<img class='img-btn-delete' src='/images/trash.png' />" +
            "</div>";
    } else {
        deleteBtnHtml = "<div id='bd-1'></div>";
    }

    var result = `<div id="edit-question-grid" style="grid-template-rows:repeat(1, 100px)">
                    <div id ="ln-@option.Id" class="list-number">1</div>
                    <textarea id="qnb-@option.Id" class="question-name-bar">Gridsbad</textarea>`
        + circleHtml + deleteBtnHtml +
        `</div>`;

    return result;
}

test('Edit Question Grid has 4 children with Multiple Choice questions', () => {
    $('body').append(setup("MC"));
    expect($('#edit-question-grid').children().length).toBe(4);
});

test('Edit Question Grid correctly added empty-circle class to circle-1', () => {
    $('body').append(setup("TF"));
    expect($("#circle-1").attr("class")).toBe("empty-circle");
});

test('Edit Question Grid correctly added btn-delete class to bd-1', () => {
    $('body').append(setup("R"));
    expect($("#bd-1").attr("class")).toBe("btn-delete");
});