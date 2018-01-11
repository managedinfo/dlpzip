# dlpzip
DLPzip
    Read config
        Read Registry for config location
        Read next config update time
        If current then 
            use local copy
        ELSE
            If URL
                Get URL
            else If path
                Read file from path
            endif
        endif
        Read contents
        Decrypt
        Parse
            Config update period
            Default password
            Allow multiple source folders (default true)
            Allow multiple files types (default true)
            Allow multiple Titus classifications (default false)
            Add Titus metadata files (default true)
            Titus metadata path (using source filename substitution)
            Titus classification on destination (default true)
            Titus classification of destination (default matches first file, or static value)
        Write next config update time
    UI needed?
    Destination exists?
    Read Titus information for destination
    Read contents of destination
    For each file
        Read Titus information
        Check if allowed in zip
        Optionally add to zip
        Optionally add metadata to zip
    Apply Titus classification to file
