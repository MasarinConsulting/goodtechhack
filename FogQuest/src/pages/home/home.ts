import { Component, ViewChild, ElementRef } from '@angular/core';
import { NavController } from 'ionic-angular';
import L from 'leaflet';

@Component({
    selector: 'page-home',
    templateUrl: 'home.html'
})
export class HomePage {
    @ViewChild('map') mapContainer: ElementRef;
    map: any;
    constructor(public navCtrl: NavController) {

    }

    ionViewDidEnter() {
        this.loadmap();
    }

    loadmap() {
      var MyIcon = L.Icon.extend({
        options: {
          iconSize: [35, 35],
          shadowSize: [50, 64],
          iconAnchor: [15 , 15],
          shadowAnchor: [4, 62],
          popupAnchor: [0, -20]
        }
      });
      var questIcon = new MyIcon({
        iconUrl: './assets/imgs/alert.png'
      });
      var infoIcon = new MyIcon({
          iconUrl: './assets/imgs/fornminne.png'
      });
      var userIcon = new MyIcon({
          iconUrl: './assets/imgs/android-locate.png'
      });
      var userMarker;

      this.map = L.map("map").fitWorld();
      
      //L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      L.tileLayer('http://{s}.tile.stamen.com/watercolor/{z}/{x}/{y}.jpg', {
            attributions: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
            maxZoom: 15,
            minZoom: 10
      }).addTo(this.map);

      var fog = L.fogLayer([
           // lat, lng
      ]).addTo(this.map);

      //platser:
      var img1url = "http://mm.dimu.org/image/04TzK16adV?dimension=145x105";
      var p1 = new L.marker([62.390666, 17.311993], { icon: questIcon }).bindPopup(
          "<img src='" + img1url + "' /><br />Hjälm 1500-tal.<br />Hittad i Granlo 1923."
      ).openPopup();
      p1.addTo(this.map);

      var img2url = "http://catview.historiska.se/catview/media/lowres/309527";
      var p2 = new L.marker([62.390969, 17.309439], { icon: questIcon }).bindPopup(
          "<img src='" + img2url + "' /><br />Tväreggad yxa av bergart<br />från yngre stenåldern.<br />Hittad i Granlo 1872."
      ).openPopup();
      p2.addTo(this.map);

      var img3url = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/67/Gustav_Adolfs_kyrka_21.JPG/1200px-Gustav_Adolfs_kyrka_21.JPG";
      var p3 = new L.marker([62.391211, 17.300008], { icon: infoIcon }).bindPopup(
          "<img src='" + img3url + "' /><br />GA-Kyrkan<br />Invigdes 2 dec år 1894<br />" +
            "Kyrktuppen är 1, 3 m hög & 1, 45 m bred<br />" +
            "Kyrktornet är 81 m högt<br />" +
            "Takspiran är 36 m från murslutet<br />" +
            "Kyrkklockans urtavlor är 3, 6 m i diameter"
      ).openPopup();
      p3.addTo(this.map);
      this.map.locate({
          setView: true,
          maxZoom: 15,
          minZoom: 10,
          watch: true
      }).on('locationfound', (e) => {

          fog.addLatLng(e.latlng);
          
          //if (userMarker) { // check
          //    this.map.removeLayer(userMarker); // remove
          //}
          //userMarker = new L.marker([e.latitude, e.longitude], { icon: userIcon }).addTo(this.map); // set

          //let markerGroup = L.featureGroup();
          //L.marker([e.latitude, e.longitude], { icon: userIcon }).addTo(this.map).on('click', () => {
          //    alert('Marker clicked');
          //})
      }).on('locationerror', (err) => {
          alert(err.message);
          });
        
    }

}
