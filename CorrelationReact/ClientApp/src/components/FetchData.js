import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = {
            startdate: "2022-08-15", enddate: "2022-09-02", loaded: false, data: {}
        };

        this.handleChangeStartDate = this.handleChangeStartDate.bind(this);
        this.handleChangeEndDate = this.handleChangeEndDate.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    static renderCorrelationData(forecast) {
        return (
            <div>
                <p>Result:</p>
                <div>USD/CAD High: {forecast.usdCad.high} - Low: {forecast.usdCad.low} - Mean average: {forecast.usdCad.meanAvg}</div>
                <div>Corra High: {forecast.corra.high} - Low: {forecast.corra.low} - Mean average: {forecast.corra.meanAvg}</div>
                <div>PearsonCoeff: {forecast.pearsonCoeff}</div>
            </div>
        );
    }

    handleChangeStartDate(event) {
        this.setState({ startdate: event.target.value });
    }

    handleChangeEndDate(event) {
        this.setState({ enddate: event.target.value });
    }

    handleSubmit(event) {
        this.populateCorrelationData();
        event.preventDefault();
    }

    render() {
        let contents = this.state.loaded
            ? (this.state.data.error ?
                this.state.data.error
                : FetchData.renderCorrelationData(this.state.data))
            : "Click submit";

        return (
            <div>
                <h1 id="tabelLabel"> Correlation</h1>
                <p>This component demonstrates fetching data from the server.</p>
                <form onSubmit={this.handleSubmit}>
                    <label>Start date
                        <input type="date" value={this.state.startdate} onChange={this.handleChangeStartDate} />
                    </label>
                    <label>End date
                        <input type="date" value={this.state.enddate} onChange={this.handleChangeEndDate} />
                    </label>
                    <input type="submit" value="Submit" />
                </form>
                {contents}
            </div>
        );
    }

    async populateCorrelationData() {
        const response = await fetch(`data?startdate=${this.state.startdate}&enddate=${this.state.enddate}`);
        const data = await response.json();
        this.setState({ data: data, loaded: true });
    }
}
