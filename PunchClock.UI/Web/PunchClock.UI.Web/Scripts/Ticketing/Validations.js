function comboboxChangeEvent(e) {
    if (this.value() && this.selectedIndex === -1) {
        showDialog("Invalid Entry!", "Please select valid option for " + $(this.element).attr("name"), 400, 600);
        this.value("");
    }
}