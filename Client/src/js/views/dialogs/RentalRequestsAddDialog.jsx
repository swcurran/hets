import React from 'react';

import { connect } from 'react-redux';

import { Form, FormGroup, HelpBlock, ControlLabel, Alert, Row, Col } from 'react-bootstrap';

import _ from 'lodash';
import Promise from 'bluebird';

import Moment from 'moment';

import * as Api from '../../api';
import * as Constant from '../../constants';

import DateControl from '../../components/DateControl.jsx';
import EditDialog from '../../components/EditDialog.jsx';
import FilterDropdown from '../../components/FilterDropdown.jsx';
import FormInputControl from '../../components/FormInputControl.jsx';
import Spinner from '../../components/Spinner.jsx';

import { isValidDate, today } from '../../utils/date';
import { isBlank } from '../../utils/string';

var RentalRequestsAddDialog = React.createClass({
  propTypes: {
    rentalRequest: React.PropTypes.object,
    currentUser: React.PropTypes.object,
    localAreas: React.PropTypes.object,
    districtEquipmentTypes: React.PropTypes.object,
    projects: React.PropTypes.object,
    project: React.PropTypes.object,
    onSave: React.PropTypes.func.isRequired,
    onClose: React.PropTypes.func.isRequired,
    show: React.PropTypes.bool,
  },

  getInitialState() {
    const { project } = this.props;
    return {
      loading: false,
      projectId: project ? project.id : 0,
      localAreaId: 0,
      equipmentTypeId: 0,
      count: 1,
      expectedHours: '',
      expectedStartDate: today(),
      expectedEndDate: '',

      projectError: '',
      localAreaError: '',
      equipmentTypeError: '',
      countError: '',
      expectedHoursError: '',
      expectedStartDateError: '',
      expectedEndDateError: '',
    };
  },

  componentDidMount() {
    this.setState({ loading: true });
    var equipmentTypesPromise = Api.getDistrictEquipmentTypes(this.props.currentUser.district.id);
    var projectsPromise = Api.searchProjects();
    Promise.all([equipmentTypesPromise, projectsPromise]).then(() => {
      this.setState({ loading: false });
    });
  },

  updateState(state, callback) {
    this.setState(state, callback);
  },

  updateEquipmentTypeState(state) {
    var selectedEquipment =_.find(this.props.districtEquipmentTypes, { id: state.equipmentTypeId });
    var isDumpTruck = selectedEquipment.equipmentType.isDumpTruck;
    var expectedHours = isDumpTruck ? 600 : 300;
    this.setState({ ...state, expectedHours });
  },

  didChange() {
    if (this.state.projectId !== 0) { return true; }
    if (this.state.localAreaId !== 0) { return true; }
    if (this.state.equipmentTypeId !== 0) { return true; }
    if (this.state.count !== 0) { return true; }
    if (this.state.expectedHours !== this.props.rentalRequest.expectedHours) { return true; }
    if (this.state.expectedStartDate !== this.props.rentalRequest.expectedStartDate) { return true; }
    if (this.state.expectedEndDate !== this.props.rentalRequest.expectedEndDate) { return true; }
    if (this.state.rentalRequestAttachments !== this.props.rentalRequest.rentalRequestAttachments) { return true; }

    return false;
  },

  isValid() {
    // Clear out any previous errors
    var valid = true;
    this.setState({
      projectError: '',
      localAreaError: '',
      equipmentTypeError: '',
      countError: '',
      expectedHoursError: '',
      expectedStartDateError: '',
      expectedEndDateError: '',
    });

    if (this.state.projectId === 0) {
      this.setState({ projectError: 'Project is required' });
      valid = false;
    }

    if (this.state.localAreaId === 0) {
      this.setState({ localAreaError: 'Local area is required' });
      valid = false;
    }

    if (this.state.equipmentTypeId === 0) {
      this.setState({ equipmentTypeError: 'Equipment type is required' });
      valid = false;
    }

    if (isBlank(this.state.count)) {
      this.setState({ countError: 'Equipment count is required' });
      valid = false;
    } else if (this.state.count < 1) {
      this.setState({ countError: 'Equipment count not valid' });
      valid = false;
    }

    if (isBlank(this.state.expectedHours)) {
      this.setState({ expectedHoursError: 'Estimated hours are required' });
      valid = false;
    } else if (this.state.expectedHours < 1) {
      this.setState({ expectedHoursError: 'Estimated hours not valid' });
      valid = false;
    }

    if (isBlank(this.state.expectedStartDate)) {
      this.setState({ expectedStartDateError: 'Start date is required' });
      valid = false;
    } else if (!isValidDate(this.state.expectedStartDate)) {
      this.setState({ expectedStartDateError: 'Date not valid' });
      valid = false;
    }

    if (Moment(this.state.expectedEndDate).isBefore(this.state.expectedStartDate)) {
      this.setState({ expectedEndDateError: 'End date must be later than the start date' });
      valid = false;
    }

    return valid;
  },

  onProjectSelected(/* project */) {
    // TODO Restrict the available local areas to a project service area
  },

  onSave() {
    this.props.onSave({
      project: { id: this.state.projectId },
      localArea: { id: this.state.localAreaId },
      districtEquipmentType: { id: this.state.equipmentTypeId },
      equipmentCount: this.state.count,
      status: Constant.RENTAL_REQUEST_STATUS_CODE_IN_PROGRESS, 
      expectedHours: this.state.expectedHours,
      expectedStartDate: this.state.expectedStartDate,
      expectedEndDate: this.state.expectedEndDate,
      rentalRequestAttachments: [{ 
        id: 0,
        attachment: this.state.rentalRequestAttachments,
      }],
    });
  },

  render() {
    if (this.state.loading) { return <div style={{ textAlign: 'center' }}><Spinner/></div>; }

    // Constrain the local area drop downs to those in the District of the current logged in user
    var localAreas = _.chain(this.props.localAreas)
      .sortBy('name')
      .value();

    var districtEquipmentTypes = _.chain(this.props.districtEquipmentTypes)
      .filter(type => type.district.id == this.props.currentUser.district.id)
      .sortBy('districtEquipmentName')
      .value();

    var projects = _.sortBy(this.props.projects.data, 'name');

    const { project } = this.props;

    return <EditDialog id="add-rental-request" show={ this.props.show }
      onClose={ this.props.onClose } onSave={ this.onSave } didChange={ this.didChange } isValid={ this.isValid }
      title= {
        <strong>Add Rental Request</strong>
      }>
      <Form>
        <Row>
          <Col md={12}>
            <FormGroup controlId="projectId" validationState={ this.state.projectError ? 'error' : null }>
              <ControlLabel>Project <sup>*</sup></ControlLabel>
              { project ?
                <div>{ project.name }</div>
                :
                <FilterDropdown id="projectId" selectedId={ this.state.projectId } onSelect={ this.onProjectSelected } updateState={ this.updateState }
                  items={ projects } className="full-width"
                />
              }
              <HelpBlock>{ this.state.projectError }</HelpBlock>
            </FormGroup>
          </Col>
          <Col md={12}>
            <FormGroup controlId="localAreaId" validationState={ this.state.localAreaError ? 'error' : null }>
              <ControlLabel>Local Area <sup>*</sup></ControlLabel>
              <FilterDropdown id="localAreaId" selectedId={ this.state.localAreaId } updateState={ this.updateState }
                items={ localAreas } className="full-width"
              />
              <HelpBlock>{ this.state.localAreaError }</HelpBlock>
            </FormGroup>
          </Col>
          <Col md={12}>
            <FormGroup controlId="equipmentTypeId" validationState={ this.state.equipmentTypeError ? 'error' : null }>
              <ControlLabel>Equipment Type <sup>*</sup></ControlLabel>
              <FilterDropdown id="equipmentTypeId" fieldName="districtEquipmentName" selectedId={ this.state.equipmentTypeId } updateState={ this.updateEquipmentTypeState }
                items={ districtEquipmentTypes } className="full-width"
              />
              <HelpBlock>{ this.state.equipmentTypeError }</HelpBlock>
            </FormGroup>
          </Col>
          <Col md={12}>
            <FormGroup controlId="count" validationState={ this.state.countError ? 'error' : null }>
              <ControlLabel>Count <sup>*</sup></ControlLabel>
              <FormInputControl type="number" min="0" value={ this.state.count } updateState={ this.updateState } />
              <HelpBlock>{ this.state.countError }</HelpBlock>
            </FormGroup>
          </Col>
          <Col md={12}>
            <FormGroup>
              <ControlLabel>Attachment(s)</ControlLabel>
              <FormInputControl id="rentalRequestAttachments" type="text" defaultValue={ this.state.rentalRequestAttachments } updateState={ this.updateState } />
            </FormGroup>
          </Col>
          <Col md={12}>
            <FormGroup controlId="expectedHours" validationState={ this.state.expectedHoursError ? 'error' : null }>
              <ControlLabel>Expected Hours <sup>*</sup></ControlLabel>
              <FormInputControl type="number" className="full-width" min={0} value={ this.state.expectedHours } updateState={ this.updateState }/>
              <HelpBlock>{ this.state.expectedHoursError }</HelpBlock>
            </FormGroup>
          </Col>
          <Col md={12}>
            <FormGroup controlId="expectedStartDate" validationState={ this.state.expectedStartDateError ? 'error' : null }>
              <ControlLabel>Start Date <sup>*</sup></ControlLabel>
              <DateControl id="expectedStartDate" date={ this.state.expectedStartDate } updateState={ this.updateState } placeholder="mm/dd/yyyy" title="Dated At" />
              <HelpBlock>{ this.state.expectedStartDateError }</HelpBlock>
            </FormGroup>
          </Col>
          <Col md={12}>
            <FormGroup controlId="expectedEndDate" validationState={ this.state.expectedEndDateError ? 'error' : null }>
              <ControlLabel>End Date</ControlLabel>
              <DateControl id="expectedEndDate" date={ this.state.expectedEndDate } updateState={ this.updateState } placeholder="mm/dd/yyyy" title="Dated At" />
              <HelpBlock>{ this.state.expectedEndDateError }</HelpBlock>
            </FormGroup>
          </Col>
        </Row>
        { this.props.rentalRequest.error &&
          <Alert bsStyle="danger">{ this.props.rentalRequest.errorMessage }</Alert>
        }
      </Form>
    </EditDialog>;
  },
});

function mapStateToProps(state) {
  return {
    rentalRequest: state.models.rentalRequest,
    currentUser: state.user,
    localAreas: state.lookups.localAreas,
    districtEquipmentTypes: state.lookups.districtEquipmentTypes.data,
    projects: state.models.projects,
  };
}

export default connect(mapStateToProps)(RentalRequestsAddDialog);