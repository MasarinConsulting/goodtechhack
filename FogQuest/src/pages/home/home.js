var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewChild, ElementRef } from '@angular/core';
import { NavController } from 'ionic-angular';
import L from 'leaflet';
var HomePage = (function () {
    function HomePage(navCtrl) {
        this.navCtrl = navCtrl;
    }
    HomePage.prototype.ionViewDidEnter = function () {
        this.loadmap();
    };
    HomePage.prototype.loadmap = function () {
        var _this = this;
        var telescopeIcon = L.icon({
            iconUrl: 'telescope.png',
            shadowUrl: 'telescope.png',
            iconSize: [38, 95],
            shadowSize: [50, 64],
            iconAnchor: [22, 94],
            shadowAnchor: [4, 62],
            popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
        });
        this.map = L.map("map").fitWorld();
        L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attributions: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
            maxZoom: 18
        }).addTo(this.map);
        this.map.locate({
            setView: true,
            maxZoom: 10
        }).on('locationfound', function (e) {
            var radius = e.accuracy / 2;
            L.marker([e.latlng], { icon: telescopeIcon }).addTo(_this.map)
                .bindPopup("You are within " + radius + " meters from this point").openPopup();
            L.circle(e.latlng, radius).addTo(_this.map);
            var markerGroup = L.featureGroup();
            var marker = L.marker([e.latitude, e.longitude]).on('click', function () {
                alert('Marker clicked');
            });
            markerGroup.addLayer(marker);
            _this.map.addLayer(markerGroup);
        }).on('locationerror', function (err) {
            alert(err.message);
        });
    };
    return HomePage;
}());
__decorate([
    ViewChild('map'),
    __metadata("design:type", ElementRef)
], HomePage.prototype, "mapContainer", void 0);
HomePage = __decorate([
    Component({
        selector: 'page-home',
        templateUrl: 'home.html'
    }),
    __metadata("design:paramtypes", [NavController])
], HomePage);
export { HomePage };
//# sourceMappingURL=home.js.map