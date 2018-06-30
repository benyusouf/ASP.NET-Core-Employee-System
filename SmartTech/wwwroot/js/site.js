// Write your Javascript code.

$(function() {
    
    //Show and hide Admin passcode base on role selected
    $('#role').change(function(){
        var adminValue = $('#role').val().toString().toLowerCase();
        if(adminValue === 'admin') {
            $('#admincode').removeClass('hide').addClass('show');
        } else {
            $('#admincode, #adminerror').removeClass('show').addClass('hide');
            
        }
    });
    
    //SOME VALIDATION
    function preventDefaultAndShowError(event, span){
        event.preventDefault();
        span.addClass('show').focus();
    }
    
    $("#register").submit(function(e){
        
        //Some variables
        var role = $('#role').val().toString().toLowerCase(),
            code = $('#admin').val().toString(),
            adminerror = $('#adminerror'),
            children = $('#children').val(),
            childrenerror = $('#childrenerror');
        
        
        //Admin Passcode Validation
        if (role === 'admin' && code !== '62829800'){
            preventDefaultAndShowError(e, adminerror);
        } 
        
        //Select Options Validation
        
        $('.custom-select').each(function () {
            var selected = $(this).val().toString().toLowerCase();
            if(selected === "select"){
                e.preventDefault();
                $(this).addClass('border-red');
            }
        });

        //Validation for number of children
        if (jQuery.type(parseInt(children)) !== 'number') {
            preventDefaultAndShowError(e, childrenerror);
        }
        
        
    });

    // Remove red border from select tag after choosing correct option
    $('.custom-select').change(function () {
        var selected = $(this).val().toString().toLowerCase();
        if(selected !== 'select'){
            $(this).removeClass('border-red');
        }
    });
    
    // Employees list Tabs
    $('ul.tabs li').click(function(){
        var tab_id = $(this).attr('data-tab');

        $('ul.tabs li').removeClass('current');
        $('.tab-content').removeClass('current');

        $(this).addClass('current');
        $("#"+tab_id).addClass('current');
    });

    // Populating postion options based on department selected
    $('#department').change(function() {
        var val = $(this).val();
        var position = $('#position');
        if (val === "select") {
            position.html('');
        } 
        else if (val === "Accounting") {
            position.html('' +
                '<option value="GM-Accounting">General Manager Accounting</option>' +
                '<option value="AGM-Accounting">Asst. General Manager Accounting</option>' +
                '<option value="Accountant-1">Accountant-I</option>' +
                '<option value="Accountant-2">Accountant-II</option>' +
                '<option value="Accountant-3">Accountant-III</option>' +
                '');
        }
        else if (val === "Administration") {
            position.html('' +
                '<option value="GM-Administration">General Manager Admin</option>' +
                '<option value="AGM-Administration">Asst. General Manager Admin</option>' +
                '<option value="Secretary">Secretary</option>' +
                '<option value="Clerk">Clerk</option>' +
                '<option value="Editor">Editor</option>' +
                '');
        }
        else if (val === "Audit") {
            position.html('' +
                '<option value="GM-Audit">General Manager Audit</option>' +
                '<option value="AGM-Audit">Asst. General Manager Audit</option>' +
                '<option value="Auditor-1">Auditor-I</option>' +
                '<option value="Auditor-2">Auditor-II</option>' +
                '<option value="Auditor-3">Auditor-III</option>' +
                '');
        }
        else if (val === "Engineering") {
            position.html('' +
                '<option value="GM-Engineering">General Manager Engineering</option>' +
                '<option value="AGM-Engineering">Asst. General Manager Engineering</option>' +
                '<option value="Engineer-1">Engineer-I</option>' +
                '<option value="Engineer-2">Engineer-II</option>' +
                '<option value="Workshop-Assistant">Workshop Assistant</option>' +
                '');
        }
        else if (val === "ICT") {
            position.html('' +
                '<option value="GM-ICT">General Manager ICT</option>' +
                '<option value="AGM-ICT">Asst. General Manager ICT</option>' +
                '<option value="Software-Engineer">Software Engineer</option>' +
                '<option value="Network-Engineer">Network Engineer</option>' +
                '<option value="Data-Analyst">Data Analyst</option>' +
                '');
        }
        else if (val === "HR") {
            position.html('' +
                '<option value="GM-HR">General Manager HR</option>' +
                '<option value="AGM-HR">Asst. General Manager HR</option>' +
                '<option value="HR-Officer">HR Officer</option>' +
                '<option value="Recruiter-1">Recruiter-I</option>' +
                '<option value="Recruiter-2">Recruiter-2</option>' +
                '');
        }
        else if (val === "Marketing") {
            position.html('' +
                '<option value="GM-Marketing">General Manager Marketing</option>' +
                '<option value="AGM-Marketing">Asst. General Manager Marketing</option>' +
                '<option value="QC-Officer">Quality Control Officer</option>' +
                '<option value="QA-Officer">Quality Assurance Officer</option>' +
                '<option value="Marketer">Marketer</option>' +
                '');
        }
        else if (val === "Operation") {
            position.html('' +
                '<option value="GM-Operation">General Manager Operation</option>' +
                '<option value="AGM-Operation">Asst. General Manager Operation</option>' +
                '<option value="Technician">Technician</option>' +
                '<option value="Field-Agent">Field Agent</option>' +
                '<option value="HD-Officer">Help Desk Officer</option>' +
                '');
        }
        else if (val === "Sales") {
            position.html('' +
                '<option value="GM-Sales">General Manager Sales</option>' +
                '<option value="AGM-Sales">Asst. General Manager Sales</option>' +
                '<option value="Promotion-Officer">Promotion Officer</option>' +
                '<option value="Sales-Agent">Sales Agent</option>' +
                '<option value="Receptionist">Receptionist</option>' +
                '');
        }
        else if (val === "Security") {
            position.html('' +
                '<option value="GM-Security">General Manager Security</option>' +
                '<option value="CS-Officer">Chief Security Officer</option>' +
                '<option value="Security-1">Security-I</option>' +
                '<option value="Security-2">Security-II</option>' +
                '<option value="Security-3">Security-III</option>' +
                '');
        }
    });
    
    // Restricting Contract Expire date selection to today and date ahead

    var expire = $('#expire');
        
    expire.datepicker({
        language: 'en',
        minDate: new Date()
    });
    
    
    // Populating Duration of Service Fields
    expire.change(function () {
        var dateJoined = $('#joined').val();
        var dateExpire = $(this).val();
        
        var values = dateDiff(dateJoined, dateExpire);
        
        $('#years').val(values[0]);
        $('#months').val(values[1]);
        $('#days').val(values[2]);
    });

    // Triggering Expire Change event
    $('#years, #months, #days').click(function() {
        $('#expire').change();
    });
    
    // Back Button
    $('#back').click(function (e) {
        e.preventDefault();
        window.history.back();
    });
    
});


// Calculating DatedJoined and ContractExpire Function Definition
function dateDiff(joined, expire) {

    joined = joined.split('/');
    expire = expire.split('/');

    var year = parseInt(expire[2]);
    var month = parseInt(expire[0]);
    var day = parseInt(expire[1]);

    var yy = parseInt(joined[2]);
    var mm = parseInt(joined[0]);
    var dd = parseInt(joined[1]);


    var years, months, days;

    // months
    months = month - mm;
    if (day < dd) {
        months = months - 1;
    }

    // years
    years = year - yy;
    if (month * 100 + day < mm * 100 + dd) {
        years = years - 1;
        months = months + 12;
    }

    // days
    days = Math.floor(((new Date(year, month-1, day)).getTime() - (new Date(yy + years, mm + months - 1, dd)).getTime()) / (24 * 60 * 60 * 1000));
    //
    
    return [years, months, days];
    
}
