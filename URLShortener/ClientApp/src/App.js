import React, { Component } from 'react';
import { Route } from 'react-router';

import { URLShortener } from './components/URLShortener';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Route exact path='/' component={URLShortener} />
    );
  }
}
