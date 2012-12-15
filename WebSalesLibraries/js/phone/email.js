(function( $ )    {
    var updateSummary = function(){
        $('#email-to-summary').html($('#email-to').val());        
        $('#email-to-copy-summary').html($('#email-to-copy').val());        
        $('#email-from-summary').html($('#email-from').val());        
        $('#email-subject-summary').html($('#email-subject').val());        
        $('#email-body-summary').html($('#email-body').val());        
    }
    
    $.sendEmail = function(linkId){
        $.ajax({
            type: "POST",
            url: "site/emailLinkSend",
            data: {
                linkId: linkId,
                emailTo: $('#email-to').val(),
                emailCopyTo: $('#email-to-copy').val(),
                emailFrom: $('#email-from').val(),
                emailToMe: $('#email-from-copy-me').is(':checked'),
                emailSubject: $('#email-subject').val(),
                emailBody: $('#email-body').val(),
                expiresIn: $('#expires-in').val()
            },
            beforeSend: function(){
                $.mobile.loading( 'show', {
                    textVisible: false,
                    html: ""
                });
            },            
            success: function(){
                $.mobile.changePage('#email-success-popup', {
                    transition: "pop"
                });                
            },
            complete: function(){
                $.mobile.loading( 'hide', {
                    textVisible: false,
                    html: ""
                });                
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $(document).ready(function() 
    {
        $( '#email-existed-list input[type="checkbox"]').checkboxradio();
        
        $( '#email-to-select-button').off('click');
        $( '#email-to-select-button').on('click',function(){
            $.mobile.changePage('#email-to-existed-list', {
                transition: "pop"
            });
        });                
        
        $( '#email-to-copy-select-button').off('click');
        $( '#email-to-copy-select-button').on('click',function(){
            $.mobile.changePage('#email-to-copy-existed-list', {
                transition: "pop"
            });
        });                
        
        $( '#email-to-apply-button').off('click');
        $( '#email-to-apply-button').on('click',function(){
            var selectedEmails = [];
            $.each($('#email-to-existed-list-container .existed-email-to:checked'),function(){
                selectedEmails.push($(this).val());
            });
            if(selectedEmails.length>0)
                $('#email-to').val(selectedEmails.join('; '));
            else
                $('#email-to').val('');            
            $( "#email-to-existed-list" ).dialog( "close" );
        });                        
        
        $( '#email-to-copy-apply-button').off('click');
        $( '#email-to-copy-apply-button').on('click',function(){
            var selectedEmails = [];
            $.each($('#email-to-copy-existed-list-container .existed-email-to-copy:checked'),function(){
                selectedEmails.push($(this).val());
            });
            if(selectedEmails.length>0)
                $('#email-to-copy').val(selectedEmails.join('; '));
            else
                $('#email-to-copy').val('');
            $( "#email-to-copy-existed-list" ).dialog( "close" );
        }); 
        
        $('#email-summary').on('pageshow', function(e){
            updateSummary();
            return true;
        });
    });
})( jQuery );