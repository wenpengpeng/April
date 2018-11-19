var orignalSetItem = localStorage.setItem;
localStorage.setItem = function (key, newValue) {
    var setItemEvent = new Event("setItemEvent");
    setItemEvent.newValue = newValue;
    window.dispatchEvent(setItemEvent);
    orignalSetItem.apply(this, arguments);
}
window.addEventListener("setItemEvent", function (e) {
    debugger;
    alert(e.newValue);
});