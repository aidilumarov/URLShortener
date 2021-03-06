﻿import React, { Component } from "react";
import "./UrlShortener.css";

export class UrlShortener extends Component {
    static displayName = UrlShortener.name;

    constructor(props) {
        super(props);
        this.shortenUrl = this.shortenUrl.bind(this);
        this.urlInputField = React.createRef();
        this.responseArea = React.createRef();
    }

    handleKeyPress = (e) => {
        if (e.keyCode === 13) {
            this.shortenUrl();
        }
    }

    shortenUrl() {
        let url = this.urlInputField.current.value;
        console.log(url);

        if (UrlShortener.isValidUrl(url)) {
            url = UrlShortener.addProtocolName(url);
            this.sendShortenUrlRequest(url);
        } else {
            this.setResponseArea("Invalid url");
        }
    }

    wrapInsideAnchor(link) {
        return `<a href="${link}">${link}</a>`
    }

    async sendShortenUrlRequest(url) {
        let requestBody = { longUrl: url };
        return fetch("urlshortener",
                {
                    method: "POST",
                    mode: "cors",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(requestBody)
                })
            .then(response => {
                console.log(response);
                return response.json();
            })
            .then(data => {
                this.setResponseArea(this.wrapInsideAnchor(data["shortUrl"]));
            });
    }

    setResponseArea(response) {
        this.responseArea.current.innerHTML = response;
        this.responseArea.current.hidden = false;
    }

    static addProtocolName(url) {
        if (!url.startsWith("http")) {
            return "https://" + url;
        }
        return url;
    }

    static isValidUrl(str) {
        var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
            '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
            '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
            '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
            '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
            '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator
        return !!pattern.test(str);
    }

    render() {
        return (
            <div className="centered">
                <h2 className="landing-header">Shorten your URLs</h2>
                <input className="url-input"
                    ref={ this.urlInputField }
                    placeholder="Input a URL to shorten here"
                    onKeyDown={this.handleKeyPress} />
                <button class="submit" onClick={this.shortenUrl} onKeyPress={this.handleKeyPress}>Shorten</button>
                <div className="response-area" hidden ref={ this.responseArea }>

                </div>

            </div>
        );
    }
}