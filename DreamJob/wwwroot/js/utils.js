var utils = {};
(function () {


    this.createInput = (parent, value, labelText, id, type, classes = []) => {

        var input = document.createElement("input");
        input.type = type;
       
        input.value = value;
        inputLabel = document.createElement("label");
        inputLabel.textContent = labelText;
        inputLabel.classList.add("control-label");
        input.id = id;
        input.classList.add("form-control");

       
        inputLabel.setAttribute("for", id);

        for (var index = 0; index < classes.length; index++) {
            input.classList.add(classes[index]);
        }


        var divForInput = document.createElement("div");
        divForInput.classList.add("form-group");

        divForInput.appendChild(inputLabel);
        divForInput.appendChild(input);

        parent.appendChild(divForInput);

        return input;
    }

    this.createButton = (type, id, text, classes, attributes = []) => {
        var button = document.createElement("button");
        button.type = type;
        button.id = id;
        button.textContent = text;
        for (var index = 0; index < classes.length; index++) {
            button.classList.add(classes[index]);
        }

        for (var index = 0; index < attributes.length; index++) {
            button.setAttribute(attributes[index]["key"], attributes[index]["value"]);
        }

        return button;
    }



}).call(utils);