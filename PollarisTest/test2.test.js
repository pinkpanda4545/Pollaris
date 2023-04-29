const jsdom = require('jsdom');
const { JSDOM } = jsdom;
const dom = new JSDOM('<!DOCTYPE html><html><body></body></html>');
const window = dom.window;
const $ = require('jquery')(window);

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
                    <div id ="ln-@option.Id" class="list-number">@i</div>
                    <textarea id="qnb-@option.Id" class="question-name-bar">@option.Name</textarea>`
        + circleHtml + deleteBtnHtml +
        `</div>`;

    return result;
}

test('MC test', () => {
    $('body').append(setup("MC"));
    expect($('#edit-question-grid').children().length).toBe(4);
});

test('TF test', () => {
    $('body').append(setup("TF"));
    expect($("#circle-1").attr("class")).toBe("empty-circle");
});

test('ranking test', () => {
    $('body').append(setup("R"));
    expect($("#bd-1").attr("class")).toBe("btn-delete");
});